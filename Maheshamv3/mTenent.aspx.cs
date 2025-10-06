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
                Bind();
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
                _DropDownListType.SelectedIndex = dt.Rows[0]["TenantType"].ToString() == "Main Tenent" ? 0 : 1;
                _DropDownListFacility.SelectedValue = dt.Rows[0]["Facility"].ToString();
                _TextName.Text = dt.Rows[0]["Name"].ToString();
                _TextBoxAadhar.Text = dt.Rows[0]["AadharNumber"].ToString();
                _TextBoxAddress.Text = dt.Rows[0]["Address"].ToString();
                _TextBoxAmount.Text = dt.Rows[0]["MonthlyRent"].ToString();
                _TextAdvPayment.Text = dt.Rows[0]["Advance"].ToString();
                _TextBoxEmail.Text = dt.Rows[0]["Email"].ToString();
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
                _LiteralMSG.Text = "<div class='p-3 mb-2 bg-danger text-white'>Please select a facility or room.</div>";
                return;
            }

            string query = String.IsNullOrEmpty(Request.QueryString["ID"])
                ? "INSERT INTO Tenant(MeterReadingStart,TenantType,Name,Mobile1,Mobile2,Email,Address,FatherName,HomeNumber,AadharNumber,PANNumber,VoterNumber,Facility,MonthlyRent,Advance,RentStart,Active) " +
                  "VALUES(@MeterReadingStart,@TenantType,@Name,@Mobile1,@Mobile2,@Email,@Address,@FatherName,@HomeNumber,@AadharNumber,@PANNumber,@VoterNumber,@Facility,@MonthlyRent,@Advance,@RentStart,1)"
                : "UPDATE Tenant SET MeterReadingStart=@MeterReadingStart,TenantType=@TenantType,Name=@Name,Mobile1=@Mobile1,Mobile2=@Mobile2,Email=@Email,Address=@Address,FatherName=@FatherName,HomeNumber=@HomeNumber,AadharNumber=@AadharNumber,PANNumber=@PANNumber,VoterNumber=@VoterNumber,Facility=@Facility,MonthlyRent=@MonthlyRent,Advance=@Advance,RentStart=@RentStart WHERE ID=" + Request.QueryString["ID"];

            Utility.ExecuteQuery(query, false,
                new SqlParameter("@MeterReadingStart", _TextBoxMeter.Text),
                new SqlParameter("@TenantType", _DropDownListType.SelectedValue),
                new SqlParameter("@Name", _TextName.Text),
                new SqlParameter("@Mobile1", _TextBoxMobile1.Text),
                new SqlParameter("@Mobile2", _TextBoxMobile2.Text),
                new SqlParameter("@Email", _TextBoxEmail.Text),
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
            _LiteralMSG.Text = "<div class='p-3 mb-2 bg-success text-white'>Tenant has been submitted successfully!</div>";
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

        protected void Bind()
        {
            if (String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                string query = _DropDownListType.SelectedIndex == 0
                    ? "SELECT f.ID, f.Building+' '+f.Location+' - '+ f.Title as Title FROM Facility f WHERE NOT ID IN (SELECT Facility FROM Tenant WHERE Active=1 AND TenantType='Main Tenent') ORDER BY ID"
                    : "SELECT f.ID, f.Building+' '+f.Location+' - '+ f.Title as Title FROM Facility f, Tenant t WHERE t.Active=1 AND f.ID = t.Facility AND TenantType='Main Tenent' ORDER BY ID";

                Utility._BindDropdown(_DropDownListFacility, query, "ID", "Title", true);
            }
            else
            {
                Utility._BindDropdown(_DropDownListFacility, "SELECT f.ID, f.Building+' '+f.Location+' - '+ f.Title as Title FROM Facility f WHERE Active=1 ORDER BY ID", "ID", "Title", true);
            }
        }
        protected void _DropDownListType_SelectedIndexChanged(object sender, EventArgs e) => Bind();
        protected void _DropDownListFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_DropDownListType.SelectedIndex == 1)
            {
                DataTable dt = Utility._GetDataTable("SELECT RentStart, MonthlyRent, MeterReadingStart FROM Tenant WHERE TenantType='Main Tenent' AND Facility=" + _DropDownListFacility.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    _TextBoxAmount.Text = dt.Rows[0]["MonthlyRent"].ToString();
                    _TextBoxMeter.Text = dt.Rows[0]["MeterReadingStart"].ToString();
                    _TextBoxStartDate.Text = Convert.ToDateTime(dt.Rows[0]["RentStart"]).ToString("yyyy-MM-dd");
                }
            }
        }

        protected void _ButtonAddMore_Click(object sender, EventArgs e)
        {
            Utility.ClearControls(this);
            Bind();
            _ButtonDocVeri.Visible = false;
            _ButtonAddMore.Visible = false;
            _ButtonSubmit.Visible = true;
            _LiteralMSG.Text = string.Empty;
        }
    }
}
