using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maheshamv3
{
    public partial class RentGenerator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utility._BindDropdown(_DropDownListFacility, "select f.ID,f.Building+' '+ f.Title+' '+f.Location as Title from facility f where  f.Active=1", "ID", "Title", true);
                _DropDownListYear.SelectedValue = DateTime.Now.Year.ToString();
                _DropDownListMonth.SelectedValue = DateTime.Now.ToString("MMM").ToUpper();
            }
        }
        protected void _ButtonSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = Utility._GetDataTable(String.Format("select ID,facility,RentStart,MonthlyRent,MeterReadingStart from Tenant where TenantType='Main Tenent' and Active=1 {0}",_DropDownListFacility.SelectedIndex==0?"": "and Facility="+_DropDownListFacility.SelectedValue));
            foreach (DataRow dr in dt.Rows) 
            {
                DataTable dtrent = Utility._GetDataTable(String.Format("Select * from Rent where Tenant={0} and rYear={1} and rMonth='{2}' and Facility={3} order by ID Desc",Convert.ToString(dr["ID"]),_DropDownListYear.SelectedValue,_DropDownListMonth.SelectedValue,_DropDownListFacility.SelectedValue));
                DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc((new DateTime(Convert.ToInt32( _DropDownListYear.SelectedValue),_DropDownListMonth.SelectedIndex+1,1)).AddMonths(-1), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                DataTable _MeterEnddt = Utility._GetDataTable(String.Format("select Reading from MeterReading where Facility={0} and Year={1} and MonthNo={2} order by ID desc", Convert.ToString(dr["Facility"]), indianTime.Year, indianTime.AddMonths(-1).Month ));
                DataTable _MeterStartdt = Utility._GetDataTable(String.Format("select Reading from MeterReading where Facility={0} and Year={1} and MonthNo={2} order by ID desc", Convert.ToString(dr["Facility"]), indianTime.Year, indianTime.Month));
                Int32 _MeterStart = _MeterStartdt.Rows.Count > 0 ? Convert.ToInt32(_MeterStartdt.Rows[0][0]) : Convert.ToInt32(dr["MeterReadingStart"]);
                Int32 _MeterEnd = _MeterEnddt.Rows.Count > 0 ? Convert.ToInt32(_MeterEnddt.Rows[0][0]) : 0;
                _MeterEnd = _MeterStart >= _MeterEnd ? _MeterStart : _MeterEnd;
                DateTime _RentStart = Convert.ToDateTime(dr["RentStart"]);
                DateTime _PeriodStart = new DateTime(Convert.ToInt32(_DropDownListYear.SelectedValue), _DropDownListMonth.SelectedIndex + 1, _RentStart.Day);
                DateTime _PeriodEnd = (_PeriodStart.AddMonths(1)).AddDays(-1);
                Int32 _MonthlyRent = Convert.ToInt32(dr["MonthlyRent"]);
                if (dtrent.Rows.Count > 0)
                {
                    Utility.ExecuteQuery("update Rent Set MeterStart=@MeterStart, MeterEnd=@MeterEnd, TotalAmount=@TotalAmount where ID=@ID",
                    false,
                    new SqlParameter("@ID", Convert.ToString(dtrent.Rows[0]["ID"])),
                    new SqlParameter("@MeterStart", _MeterStart),
                    new SqlParameter("@MeterEnd", _MeterEnd),
                    new SqlParameter("@TotalAmount", _MonthlyRent + (_MeterEnd - _MeterStart > 0 ? (_MeterEnd - _MeterStart) * 7 : 0)));
                }
                else
                {
                    Utility.ExecuteQuery("insert into Rent(Facility,Tenant,Amount,PeriodStart,PeriodEnd,MeterEnd,MeterStart,rMonth,rYear,rMonthNo,TotalAmount,Active) values (@Facility,@Tenant,@Amount,@PeriodStart,@PeriodEnd,@MeterEnd,@MeterStart,@rMonth,@rYear,@rMonthNo,@TotalAmount,1)",
                    false,
                    new SqlParameter("@Facility", Convert.ToString(dr["Facility"])),
                    new SqlParameter("@Tenant", Convert.ToString(dr["ID"])),
                    new SqlParameter("@Amount", _MonthlyRent),
                    new SqlParameter("@PeriodStart", String.Format("{0}-{1}-{2}", _PeriodStart.Year, _PeriodStart.Month, _PeriodStart.Day)),
                    new SqlParameter("@PeriodEnd", String.Format("{0}-{1}-{2}", _PeriodEnd.Year, _PeriodEnd.Month, _PeriodEnd.Day)),
                    new SqlParameter("@MeterStart", _MeterStart),
                    new SqlParameter("@MeterEnd", _MeterEnd),
                    new SqlParameter("@rMonth", _DropDownListMonth.SelectedValue),
                    new SqlParameter("@rYear", _DropDownListYear.SelectedValue),
                    new SqlParameter("@rMonthNo", _DropDownListMonth.SelectedIndex + 1),
                    new SqlParameter("@TotalAmount", _MonthlyRent + (_MeterEnd - _MeterStart > 0 ? (_MeterEnd - _MeterStart) * 7 : 0)));
                }
            }
            _LiteralMSG.Text = "<div class='p-3 mb-2 bg-success text-white'>Rent has been Generated Successfully!</div>";
            _ButtonNewReading.Visible = true;
            _ButtonSubmit.Visible = false;
        }

        protected void _ButtonNewReading_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RentGenerator.aspx");
        }
    }
}