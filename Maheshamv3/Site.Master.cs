using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maheshamv3
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _LiteralLog.Text = " <a href='authlogin.aspx' class='btn btn-primary rounded-pill text-white py-2 px-4 flex-wrap flex-sm-shrink-0'>Login</a>";
        }
    }
}