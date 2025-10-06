using System;
using System.Collections.Generic;
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
                Utility._BindGridView(GridView2, "select f.Building+' '+ f.Title+' '+f.Location as facility , t.*,  FORMAT(RentStart, 'dd/MM/yyyy') as StartOn  from Tenant t, facility f where f.id=t.facility and t.TenantType='Main Tenent' and t.Active=1  order by f.ID");
            }
        }

        protected void _ImageButtonView_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton _imgBtn = (ImageButton)sender;
            Response.Redirect("~/mTenent.aspx?ID="+ _imgBtn.CommandArgument);
        }
    }
}