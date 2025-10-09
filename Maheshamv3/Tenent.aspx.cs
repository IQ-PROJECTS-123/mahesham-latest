using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maheshamv3
{
    public partial class Tenent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                Utility._BindGridView(GridView2, "select f.Building+' '+ f.Title+' '+f.Location as facility , t.*,  FORMAT(RentStart, 'dd/MM/yyyy') as StartOn  from Tenant t, facility f where f.id=t.facility and t.TenantType='Main Tenent' and t.Active=1  order by f.ID");
            }
        }
        private void BindGrid()
        {
            Utility._BindGridView(GridView2,"SELECT f.Building + ' ' + f.Title + ' ' + f.Location AS facility, " + "t.*, FORMAT(RentStart, 'dd/MM/yyyy') AS StartOn " + "FROM Tenant t INNER JOIN Facility f ON f.Id = t.Facility " + "WHERE t.TenantType = 'Main Tenent' AND t.Active = 1 " + "ORDER BY f.ID");
        }
        protected void _ImageButtonView_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton _imgBtn = (ImageButton)sender;
            Response.Redirect("~/mTenent.aspx?ID="+ _imgBtn.CommandArgument);
        }
        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int tenantId = Convert.ToInt32(btn.CommandArgument);  
            DataTable dt = Utility._GetDataTable(@"SELECT t.Name AS TenantName,t.Email,t.Mobile1,f.Building + ' ' + f.Title + ' ' + f.Location AS RoomName FROM Tenant t INNER JOIN Facility f ON f.Id = t.Facility WHERE t.ID = " + tenantId);
            if (dt.Rows.Count == 0) return;
            string tenantName = dt.Rows[0]["TenantName"].ToString();
            string tenantEmail = dt.Rows[0]["Email"].ToString();
            string room = dt.Rows[0]["RoomName"].ToString();
            Utility.ExecuteQuery("UPDATE Tenant SET Active = 0 WHERE ID = @ID",false,
            new SqlParameter("@ID", tenantId));
            string subject = "Room Vacate - Mahesham Lodge";
            string emailBody = $@"
          <h3>Tenant Deactivated Notification</h3>
          <table border='1' cellpadding='6' cellspacing='0' style='border-collapse:collapse;width:60%;font-family:Arial;font-size:14px;'>
            <tr><td><b>Tenant</b></td><td>{tenantName}</td></tr>
            <tr><td><b>Room</b></td><td>{room}</td></tr>
            <tr><td><b>Status</b></td><td><span style='color:red;font-weight:bold;'>Deactivated</span></td></tr>
            <tr><td><b>Date</b></td><td>{DateTime.Now:dd/MM/yyyy HH:mm}</td></tr>
          </table>
          <p>This tenant has been deactivated in the system.</p>";
            Utility._SendEmail("rajnish5454kumar@gmail.com", tenantEmail, subject, emailBody);
            BindGrid();
        }
    }
}