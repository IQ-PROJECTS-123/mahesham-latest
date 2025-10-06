using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maheshamv3
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                _LiteralTotalRooms.Text = Convert.ToString(Utility._GetDataTable("Select COUNT(ID) as Total from facility where Active=1").Rows[0][0]);
                _Literalvacant.Text = Convert.ToString(Utility._GetDataTable("select COUNT(ID) as Vacant from facility   where not ID in (select facility from Tenant where Active=1  and TenantType='Main Tenent')").Rows[0][0]);
               // _LiteralPending.Text = Convert.ToString(Utility._GetDataTable("").Rows[0][0]);
               // _LiteralDone.Text = Convert.ToString(Utility._GetDataTable("").Rows[0][0]);
            }
        }
    }
}