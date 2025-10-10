using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maheshamv3
{
    public partial class authlogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (Request.Cookies["userInfo"] != null)
                    Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);

        }
    
        protected void ButtonSign_Click(object sender, EventArgs e)
        {
            Boolean IsAdmin = true;
            //String _dTable = _DropDownListType.SelectedIndex == 1 ? "Mentors" : "Student";
            System.Data.DataTable dt = Utility._GetDataTable(String.Format("select * from Management where (Phone='{0}' or Email='{0}') and PWD='{1}'", _TextBoxUser.Text.Trim(), _TextBoxPWD.Text));
            if (dt.Rows.Count == 0)
            {
               // dt = Utility._GetDataTable(String.Format("select *,ID,Name,PhotoURL from {0} where (Mobile='{1}' or Email='{1}') and PWD='{2}'", "orbexcoi_rpa.Mentors", _TextBoxUser.Text.Trim(), _TextBoxPWD.Text));
               // IsMentor = true;
            }
            try
            {
                if (dt.Rows.Count > 0)
                {

                    HttpCookie userInfo = new HttpCookie("userInfo");
                    userInfo["auth"] = Session.SessionID;
                    userInfo["uID"] = Convert.ToString(dt.Rows[0]["ID"]);
                   // userInfo["uPhoto"] = Convert.ToString(dt.Rows[0]["PhotoURL"]);
                    userInfo["uName"] = Convert.ToString(dt.Rows[0]["Name"]);
                    userInfo["IsAdmin"] = "A";
                    userInfo["LoginType"] = "L";
                    userInfo.Expires.Add(new TimeSpan(2, 0, 0));
                    Response.Cookies.Add(userInfo);
                    if (IsAdmin)
                        Response.Redirect("~/Dashboard.aspx", false);
                    else
                    {
                        if (!Convert.ToBoolean(dt.Rows[0]["Active"]))
                        {
                            Utility._SendEmail("shrikantkumar.info@gmail.com", "", Convert.ToString(dt.Rows[0]["Name"]) + " Login at " + DateTime.Today.ToString(), Convert.ToString(dt.Rows[0]["Name"]) + " Login at " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                            Response.Redirect("~/ledger.aspx");
                        }
                        else
                            Response.Redirect("~/default.aspx");
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
    }
}