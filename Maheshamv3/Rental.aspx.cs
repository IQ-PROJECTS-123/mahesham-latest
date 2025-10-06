using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maheshamv3
{
    public partial class Rental : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                _DropDownListYear.SelectedValue = DateTime.Now.Year.ToString();
                _DropDownListMonth.SelectedValue = DateTime.Now.ToString("MMM").ToUpper();
                _Bind();
            }
        }
        protected void _Bind()
        {
            Utility._BindGridView(GridView2,String.Format("select f.Building+' '+ f.Title+' '+f.Location as facility,t.Name,t.Mobile1, FORMAT(r.PeriodStart, 'dd/MM/yyyy') as PeriodStart,FORMAT(r.PeriodEnd, 'dd/MM/yyyy') as PeriodEnd, r.Amount,r.MeterStart,r.MeterEnd, r.MeterEnd-r.MeterStart as Unit,(r.MeterEnd-r.MeterStart)*7 as Bill ,r.TotalAmount,r.ID from Rent r,Tenant t,facility f where r.Facility =f.ID and r.Tenant=t.ID and t.Active=1 and t. TenantType='Main Tenent' and r.rYear={0} and r.rMonthNo={1}", _DropDownListYear.SelectedValue,_DropDownListMonth.SelectedIndex+1));
        }
        protected void _DropDownListMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Bind();
        }

        protected void _ImageButtonView_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton _Img = (ImageButton)sender;
            Response.Redirect("~/Payment.aspx?ID="+_Img.CommandArgument);
        }
    }
}