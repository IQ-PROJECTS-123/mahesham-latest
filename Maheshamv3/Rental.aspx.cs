using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maheshamv3
{
    public partial class Rental : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _DropDownListYear.SelectedValue = DateTime.Now.Year.ToString();
                _DropDownListMonth.SelectedValue = DateTime.Now.ToString("MMM").ToUpper();
                BindGrid();
            }
        }

        protected void BindGrid()
        {
            string query = $@"
            SELECT 
                f.Building + ' ' + f.Title + ' ' + f.Location AS facility,
                t.Name,
                t.Mobile1,
                FORMAT(r.PeriodStart, 'dd/MM/yyyy') AS PeriodStart,
                FORMAT(r.PeriodEnd, 'dd/MM/yyyy') AS PeriodEnd,
                r.Amount,
                r.MeterStart,
                r.MeterEnd,
                (r.MeterEnd - r.MeterStart) AS Unit,
                (r.MeterEnd - r.MeterStart) * 7 AS Bill,
                r.TotalAmount,
                r.ID
            FROM Rent r
            INNER JOIN Tenant t ON r.Tenant = t.ID
            INNER JOIN Facility f ON r.Facility = f.ID
            WHERE t.Active = 1
              AND t.TenantType = 'Main Tenant'  
              AND r.rYear = {_DropDownListYear.SelectedValue}
              AND r.rMonthNo = {_DropDownListMonth.SelectedIndex + 1}
            ORDER BY t.Name";

            Utility._BindGridView(GridView2, query);
        }

        protected void _DropDownListMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void _DropDownListYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void _ImageButtonView_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgBtn = (ImageButton)sender;
            Response.Redirect("~/Payment.aspx?ID=" + imgBtn.CommandArgument);
        }
    }
}
