using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maheshamv3
{
    public partial class mTenent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRooms();
                if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    LoadTenantData(Request.QueryString["ID"]);
                }
            }
        }

        private void LoadTenantData(string tenantId)
        {
            DataTable dt = Utility._GetDataTable("SELECT *, FORMAT(RentStart,'yyyy-MM-dd') as StartOn FROM Tenant WHERE ID=" + tenantId);
            if (dt.Rows.Count > 0)
            {
                _DropDownListType.SelectedIndex = dt.Rows[0]["TenantType"].ToString() == "Main Tenant" ? 0 : 1;
                _DropDownListFacility.SelectedValue = dt.Rows[0]["Facility"].ToString();
                _TextName.Text = dt.Rows[0]["Name"].ToString();
                _TextBoxAadhar.Text = dt.Rows[0]["AadharNumber"].ToString();
                _TextBoxAddress.Text = dt.Rows[0]["Address"].ToString();
                _TextBoxAmount.Text = dt.Rows[0]["MonthlyRent"].ToString();
                _TextAdvPayment.Text = dt.Rows[0]["Advance"].ToString();
                _TextBoxEmail.Text = dt.Rows[0]["Email"].ToString();
                _TextBoxPWD.Text = dt.Rows[0]["PWD"].ToString();  
                _TextBoxFather.Text = dt.Rows[0]["FatherName"].ToString();
                _TextBoxFContact.Text = dt.Rows[0]["HomeNumber"].ToString();
                _TextBoxMeter.Text = dt.Rows[0]["MeterReadingStart"].ToString();
                _TextBoxMobile1.Text = dt.Rows[0]["Mobile1"].ToString();
                _TextBoxMobile2.Text = dt.Rows[0]["Mobile2"].ToString();
                _TextBoxPAN.Text = dt.Rows[0]["PANNumber"].ToString();
                _TextBoxStartDate.Text = dt.Rows[0]["StartOn"].ToString();
                _TextBoxVoter.Text = dt.Rows[0]["VoterNumber"].ToString();
            }
        }

        protected void _ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (_DropDownListFacility.SelectedIndex == 0)
            {
                _LiteralMSG.Text = "<div class='p-3 mb-2 bg-danger text-white'>Please select a room.</div>";
                return;
            }

            if (_DropDownListType.SelectedValue == "Main Tenant")
            {
                DataTable dtCheck = Utility._GetDataTable("SELECT * FROM Tenant WHERE Facility=" + _DropDownListFacility.SelectedValue + " AND TenantType='Main Tenant' AND Active=1");
                if (dtCheck.Rows.Count > 0)
                {
                    _LiteralMSG.Text = "<div class='p-3 mb-2 bg-danger text-white'>Main Tenant already exists in this room. Please select Partner Tenant.</div>";
                    return;
                }
            }

            string tenantId = Request.QueryString["ID"];
            string password = _TextBoxPWD.Text.Trim();

            if (!string.IsNullOrEmpty(tenantId) && string.IsNullOrEmpty(password))
            {
                DataTable dtOld = Utility._GetDataTable("SELECT PWD FROM Tenant WHERE ID=" + tenantId);
                if (dtOld.Rows.Count > 0)
                {
                    password = dtOld.Rows[0]["PWD"].ToString();
                }
            }

            if (string.IsNullOrEmpty(password))
            {
                _LiteralMSG.Text = "<div class='p-3 mb-2 bg-danger text-white'>Please enter a password!</div>";
                return;
            }

            // ✅ Corrected INSERT query (PWD column fixed)
            string query = string.IsNullOrEmpty(tenantId)
                ? "INSERT INTO Tenant(MeterReadingStart,TenantType,Name,Mobile1,Mobile2,Email,PWD,Address,FatherName,HomeNumber,AadharNumber,PANNumber,VoterNumber,Facility,MonthlyRent,Advance,RentStart,Active) " +
                  "VALUES(@MeterReadingStart,@TenantType,@Name,@Mobile1,@Mobile2,@Email,@PWD,@Address,@FatherName,@HomeNumber,@AadharNumber,@PANNumber,@VoterNumber,@Facility,@MonthlyRent,@Advance,@RentStart,1)"
                : "UPDATE Tenant SET MeterReadingStart=@MeterReadingStart, TenantType=@TenantType, Name=@Name, Mobile1=@Mobile1, Mobile2=@Mobile2, Email=@Email, PWD=@PWD, Address=@Address, FatherName=@FatherName, HomeNumber=@HomeNumber, AadharNumber=@AadharNumber, PANNumber=@PANNumber, VoterNumber=@VoterNumber, Facility=@Facility, MonthlyRent=@MonthlyRent, Advance=@Advance, RentStart=@RentStart WHERE ID=" + tenantId;

            Utility.ExecuteQuery(query, false,
                new SqlParameter("@MeterReadingStart", _TextBoxMeter.Text),
                new SqlParameter("@TenantType", _DropDownListType.SelectedValue),
                new SqlParameter("@Name", _TextName.Text),
                new SqlParameter("@Mobile1", _TextBoxMobile1.Text),
                new SqlParameter("@Mobile2", _TextBoxMobile2.Text),
                new SqlParameter("@Email", _TextBoxEmail.Text),
                new SqlParameter("@PWD", password),
                new SqlParameter("@Advance", _TextAdvPayment.Text),
                new SqlParameter("@Address", _TextBoxAddress.Text),
                new SqlParameter("@FatherName", _TextBoxFather.Text),
                new SqlParameter("@HomeNumber", _TextBoxFContact.Text),
                new SqlParameter("@AadharNumber", _TextBoxAadhar.Text),
                new SqlParameter("@PANNumber", _TextBoxPAN.Text),
                new SqlParameter("@VoterNumber", _TextBoxVoter.Text),
                new SqlParameter("@Facility", _DropDownListFacility.SelectedValue),
                new SqlParameter("@MonthlyRent", _TextBoxAmount.Text),
                new SqlParameter("@RentStart", _TextBoxStartDate.Text)
            );

            _LiteralMSG.Text = "<div class='p-3 mb-2 bg-success text-white'>Tenant submitted successfully!</div>";
            _ButtonSubmit.Visible = false;
            _ButtonDocVeri.Visible = true;
            _ButtonAddMore.Visible = true;
        }

        protected void _ButtonDocVeri_Click(object sender, EventArgs e)
        {
            string tenantId = String.IsNullOrEmpty(Request.QueryString["ID"]) ? GetLastInsertedTenantID() : Request.QueryString["ID"];
            Response.Redirect("~/KYCDoc.aspx?TenantID=" + tenantId);
        }

        private string GetLastInsertedTenantID()
        {
            DataTable dt = Utility._GetDataTable("SELECT TOP 1 ID FROM Tenant ORDER BY ID DESC");
            return dt.Rows.Count > 0 ? dt.Rows[0]["ID"].ToString() : "0";
        }

        protected void BindRooms()
        {
            string query = "";
            if (_DropDownListType.SelectedValue == "Main Tenant")
            {
                query = @"SELECT f.ID, f.Building+' '+f.Location+' - '+ f.Title as Title 
                          FROM Facility f 
                          WHERE NOT ID IN (SELECT Facility FROM Tenant WHERE Active=1 AND TenantType='Main Tenant') 
                          ORDER BY ID";
            }
            else
            {
                query = @"SELECT DISTINCT f.ID, f.Building+' '+f.Location+' - '+ f.Title as Title 
                          FROM Facility f
                          INNER JOIN Tenant t ON t.Facility=f.ID
                          WHERE t.Active=1 AND t.TenantType='Main Tenant'
                          ORDER BY f.ID";
            }

            Utility._BindDropdown(_DropDownListFacility, query, "ID", "Title", true);
        }

        protected void _DropDownListType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRooms();
        }

        protected void _DropDownListFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_DropDownListType.SelectedValue == "Partner Tenant")
            {
                if (!string.IsNullOrEmpty(_DropDownListFacility.SelectedValue))
                {
                    string query = "SELECT RentStart, MonthlyRent, MeterReadingStart FROM Tenant WHERE TenantType='Main Tenant' AND Facility=" + _DropDownListFacility.SelectedValue;
                    DataTable dt = Utility._GetDataTable(query);
                    if (dt.Rows.Count > 0)
                    {
                        _TextBoxAmount.Text = dt.Rows[0]["MonthlyRent"].ToString();
                        _TextBoxMeter.Text = dt.Rows[0]["MeterReadingStart"].ToString();
                        _TextBoxStartDate.Text = Convert.ToDateTime(dt.Rows[0]["RentStart"]).ToString("yyyy-MM-dd");
                    }
                }
            }
        }

        protected void _ButtonAddMore_Click(object sender, EventArgs e)
        {
            Utility.ClearControls(this);
            BindRooms();
            _ButtonDocVeri.Visible = false;
            _ButtonAddMore.Visible = false;
            _ButtonSubmit.Visible = true;
            _LiteralMSG.Text = string.Empty;
        }
    }
}
