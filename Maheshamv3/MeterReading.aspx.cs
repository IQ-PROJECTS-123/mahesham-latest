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
    public partial class MeterReading : System.Web.UI.Page
    {
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddMonths(-1), INDIAN_ZONE);
                _DropDownListMonth.SelectedValue = indianTime.ToString("MMM").ToUpper();
                _DropDownListYear.SelectedValue = indianTime.ToString("yyyy");
                bind();
            }
        }
        protected void bind()
        {
            _LiteralMSG.Text = "";
            Boolean _ALL = _DropDownListFacility.SelectedValue == "1000";
            Utility._BindDropdown(_DropDownListFacility,String.Format("select f.ID,f.Building+' '+ f.Title+' '+f.Location as Title from facility f where  f.Active=1 {0} Order by ID",_ALL?"": string.Format("and not f.ID in (select Facility from MeterReading where Year={0} and Monthno={1})", _DropDownListYear.SelectedValue,_DropDownListMonth.SelectedIndex+1)), "ID", "Title", true);
            if (!_ALL)
                _DropDownListFacility.Items.Add(new ListItem("Show ALL", "1000"));
        }
        protected void _ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (_DropDownListFacility.SelectedIndex == 0)
            {
                _LiteralMSG.Text = "<div class='p-3 mb-2 bg-danger text-white'>Please select a facility or room.</div>";
                return;
            }

            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            string _FileName = string.Empty;
            if (_FileUpload.HasFile)
            {
                string _Extension = System.IO.Path.GetExtension(_FileUpload.FileName);
                _FileName = _DropDownListFacility.SelectedValue + "_" + _DropDownListMonth.SelectedValue + indianTime.Year.ToString() + (string.IsNullOrEmpty(_Extension) ? ".jpg" : _Extension);
                _FileUpload.SaveAs(Server.MapPath("~/MeterReading/" + _FileName));
            }
            Utility.ExecuteQuery(@"
                       IF EXISTS (SELECT 1 FROM MeterReading WHERE YEAR=@YEAR AND MonthNo=@MonthNo AND Facility=@Facility)
                        UPDATE MeterReading 
                       SET Reading=@Reading, Readingfile=@Readingfile, Readingon=@Readingon, Readingby=@Readingby 
                        WHERE YEAR=@YEAR AND MonthNo=@MonthNo AND Facility=@Facility;
            ELSE
                       INSERT INTO MeterReading(Facility, Reading, Readingfile, Readingon, Readingby, Year, MonthNo, Active)
                       VALUES(@Facility, @Reading, @Readingfile, @Readingon, @Readingby, @YEAR, @MonthNo, 1);",
                false,
                new SqlParameter("@Facility", _DropDownListFacility.SelectedValue),
                new SqlParameter("@Reading", _TextBoxReading.Text),
                new SqlParameter("@Readingfile", _FileName),
                new SqlParameter("@Readingon", indianTime),
                new SqlParameter("@YEAR", _DropDownListYear.SelectedValue),
                new SqlParameter("@MonthNo", _DropDownListMonth.SelectedIndex + 1),
                new SqlParameter("@Readingby", "Shrikant")
            );
            DataTable tenants = Utility._GetDataTable(
                    $@"SELECT ID, Facility, RentStart, MonthlyRent, MeterReadingStart 
                      FROM Tenant 
                    WHERE Facility={_DropDownListFacility.SelectedValue} 
                     AND TenantType='Main Tenent' 
                     AND Active=1");

            foreach (DataRow dr in tenants.Rows)
            {
                int tenantId = Convert.ToInt32(dr["ID"]);
                int facilityId = Convert.ToInt32(dr["Facility"]);
                int monthlyRent = Convert.ToInt32(dr["MonthlyRent"]);
                DateTime rentStart = Convert.ToDateTime(dr["RentStart"]);

                // Previous Month
                DateTime prevMonth = new DateTime(Convert.ToInt32(_DropDownListYear.SelectedValue), _DropDownListMonth.SelectedIndex + 1, 1).AddMonths(-1);

                // Previous Due
                DataTable dtrentDue = Utility._GetDataTable(
                    $@"SELECT TOP 1 Due 
               FROM Rent 
               WHERE Tenant={tenantId} AND rYear={prevMonth.Year} AND rMonthNo={prevMonth.Month} 
                 AND Facility={facilityId} ORDER BY ID DESC");
                Decimal _PrevDue = dtrentDue.Rows.Count > 0 ? Convert.ToDecimal(dtrentDue.Rows[0][0]) : 0;
                DataTable prevMeterDt = Utility._GetDataTable(
                    $"SELECT TOP 1 Reading FROM MeterReading WHERE Facility={facilityId} AND Year={prevMonth.Year} AND MonthNo={prevMonth.Month} ORDER BY ID DESC");
                int meterStart = prevMeterDt.Rows.Count > 0 ? Convert.ToInt32(prevMeterDt.Rows[0][0]) : Convert.ToInt32(dr["MeterReadingStart"]);
                int meterEnd = Convert.ToInt32(_TextBoxReading.Text);
                meterEnd = meterStart >= meterEnd ? meterStart : meterEnd;
                DateTime periodStart = new DateTime(Convert.ToInt32(_DropDownListYear.SelectedValue), _DropDownListMonth.SelectedIndex + 1, rentStart.Day);
                DateTime periodEnd = periodStart.AddMonths(1).AddDays(-1);
               // decimal paid = 0; //string.IsNullOrEmpty(_TextBoxPaid.Text) ? 0 : Convert.ToDecimal(_TextBoxPaid.Text);
                int ebill = (meterEnd - meterStart) > 0 ? (meterEnd - meterStart) * 7 : 0;
                decimal totalAmount = _PrevDue + monthlyRent + ebill;
                decimal due = totalAmount - 0;
                DataTable tenantRent = Utility._GetDataTable(
                    $@"SELECT * FROM Rent 
                   WHERE Tenant={tenantId} 
                    AND rYear={_DropDownListYear.SelectedValue} 
                    AND rMonthNo={_DropDownListMonth.SelectedIndex + 1} 
                    AND Facility={facilityId}");
                if (tenantRent.Rows.Count > 0)
                {
                    Utility.ExecuteQuery(@"UPDATE Rent 
                                   SET MeterStart=@MeterStart, MeterEnd=@MeterEnd, 
                                       TotalAmount=@TotalAmount, PaidAmount=@PaidAmount, Due=@Due,
                                       Amount=@Amount, PeriodStart=@PeriodStart, PeriodEnd=@PeriodEnd
                                      
                                   WHERE ID=@ID", false,
                        new SqlParameter("@ID", tenantRent.Rows[0]["ID"]),
                        new SqlParameter("@MeterStart", meterStart),
                        new SqlParameter("@MeterEnd", meterEnd),
                        new SqlParameter("@TotalAmount", totalAmount),
                        new SqlParameter("@Due", due),
                        new SqlParameter("@Amount", monthlyRent),
                        new SqlParameter("@PeriodStart", periodStart),
                        new SqlParameter("@PeriodEnd", periodEnd)
                       // new SqlParameter("@Status", paid > 0 ? "Completed" : "Pending")
                    );
                }
                else
                {
                 Utility.ExecuteQuery(@"INSERT INTO Rent(Facility,Tenant,Amount,PeriodStart,PeriodEnd,
                                                     MeterStart,MeterEnd,rMonth,rYear,rMonthNo,
                                                     TotalAmount,Active,Due,Status) 
                                   VALUES(@Facility,@Tenant,@Amount,@PeriodStart,@PeriodEnd,
                                          @MeterStart,@MeterEnd,@rMonth,@rYear,@rMonthNo,
                                          @TotalAmount,1,@Due,@Status)", false,
                        new SqlParameter("@Facility", facilityId),
                        new SqlParameter("@Tenant", tenantId),
                        new SqlParameter("@Amount", monthlyRent),
                        new SqlParameter("@PeriodStart", periodStart),
                        new SqlParameter("@PeriodEnd", periodEnd),
                        new SqlParameter("@MeterStart", meterStart),
                        new SqlParameter("@MeterEnd", meterEnd),
                        new SqlParameter("@rMonth", _DropDownListMonth.SelectedValue),
                        new SqlParameter("@rYear", _DropDownListYear.SelectedValue),
                        new SqlParameter("@rMonthNo", _DropDownListMonth.SelectedIndex + 1),
                        new SqlParameter("@TotalAmount", totalAmount),                        
                        new SqlParameter("@Due", due),
                        new SqlParameter("@Status",  "Pending")
                    );
                }
            }

            _LiteralMSG.Text = "<div class='p-3 mb-2 bg-success text-white'>Meter Reading & Rent have been Submitted Successfully!</div>";
            _ButtonNewReading.Visible = true;
            _ButtonSubmit.Visible = false;
        }
        protected void _ButtonNewReading_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MeterReading.aspx");

        }

        protected void _DropDownListType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind();
        }

        protected void _DropDownListFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(_DropDownListFacility.SelectedValue == "1000")
                bind();
        }
    }
}