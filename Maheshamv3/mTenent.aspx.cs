﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Maheshamv3
{
    public partial class mTenent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRooms();
                string tenantId = Request.QueryString["ID"];
                if (!string.IsNullOrEmpty(tenantId))
                {
                    LoadTenantData(tenantId);
                }
            }
        }
        private void LoadTenantData(string tenantId)
        {
            string query = $"SELECT *, FORMAT(RentStart,'yyyy-MM-dd') AS StartOn FROM Tenant WHERE ID={tenantId}";
            DataTable dt = Utility._GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                _DropDownListType.SelectedValue = row["TenantType"].ToString();
                _DropDownListFacility.SelectedValue = row["Facility"].ToString();

                _TextName.Text = row["Name"].ToString();
                _TextBoxEmail.Text = row["Email"].ToString();
                _TextBoxPWD.Text = row["PWD"].ToString();
                _TextBoxMobile1.Text = row["Mobile1"].ToString();
                _TextBoxMobile2.Text = row["Mobile2"].ToString();
                _TextBoxFather.Text = row["FatherName"].ToString();
                _TextBoxFContact.Text = row["HomeNumber"].ToString();
                _TextBoxAddress.Text = row["Address"].ToString();
                _TextBoxAadhar.Text = row["AadharNumber"].ToString();
                _TextBoxPAN.Text = row["PANNumber"].ToString();
                _TextBoxVoter.Text = row["VoterNumber"].ToString();
                _TextBoxAmount.Text = row["MonthlyRent"].ToString();
                _TextAdvPayment.Text = row["Advance"].ToString();
                _TextBoxMeter.Text = row["MeterReadingStart"].ToString();
                _TextBoxStartDate.Text = row["StartOn"].ToString();
            }
        }
        protected void _ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (_DropDownListFacility.SelectedIndex == 0)
            {
                ShowMessage("Please select a room.", false);
                return;
            }

            if (_DropDownListType.SelectedValue == "Main Tenant")
            {
                string checkQuery = $"SELECT * FROM Tenant WHERE Facility={_DropDownListFacility.SelectedValue} AND TenantType='Main Tenant' AND Active=1";
                if (Utility._GetDataTable(checkQuery).Rows.Count > 0)
                {
                    ShowMessage("Main Tenant already exists in this room. Please select Partner Tenant.", false);
                    return;
                }
            }

            string tenantId = Request.QueryString["ID"];
            string password = _TextBoxPWD.Text.Trim();

            if (!string.IsNullOrEmpty(tenantId) && string.IsNullOrEmpty(password))
            {
                DataTable dtOld = Utility._GetDataTable($"SELECT PWD FROM Tenant WHERE ID={tenantId}");
                if (dtOld.Rows.Count > 0)
                    password = dtOld.Rows[0]["PWD"].ToString();
            }

            if (string.IsNullOrEmpty(password))
            {
                ShowMessage("Please enter a password!", false);
                return;
            }

            string query = string.IsNullOrEmpty(tenantId)
                ? @"INSERT INTO Tenant(MeterReadingStart, TenantType, Name, Mobile1, Mobile2, Email, PWD, Address, FatherName, HomeNumber, 
                                        AadharNumber, PANNumber, VoterNumber, Facility, MonthlyRent, Advance, RentStart, Active)
                   VALUES(@MeterReadingStart, @TenantType, @Name, @Mobile1, @Mobile2, @Email, @PWD, @Address, @FatherName, @HomeNumber, 
                          @AadharNumber, @PANNumber, @VoterNumber, @Facility, @MonthlyRent, @Advance, @RentStart, 1)"
                : @"UPDATE Tenant SET MeterReadingStart=@MeterReadingStart, TenantType=@TenantType, Name=@Name, Mobile1=@Mobile1, Mobile2=@Mobile2, 
                       Email=@Email, PWD=@PWD, Address=@Address, FatherName=@FatherName, HomeNumber=@HomeNumber, 
                       AadharNumber=@AadharNumber, PANNumber=@PANNumber, VoterNumber=@VoterNumber, Facility=@Facility, 
                       MonthlyRent=@MonthlyRent, Advance=@Advance, RentStart=@RentStart WHERE ID=" + tenantId;

            Utility.ExecuteQuery(query, false,
                new SqlParameter("@MeterReadingStart", _TextBoxMeter.Text),
                new SqlParameter("@TenantType", _DropDownListType.SelectedValue),
                new SqlParameter("@Name", _TextName.Text),
                new SqlParameter("@Mobile1", _TextBoxMobile1.Text),
                new SqlParameter("@Mobile2", _TextBoxMobile2.Text),
                new SqlParameter("@Email", _TextBoxEmail.Text),
                new SqlParameter("@PWD", password),
                new SqlParameter("@Address", _TextBoxAddress.Text),
                new SqlParameter("@FatherName", _TextBoxFather.Text),
                new SqlParameter("@HomeNumber", _TextBoxFContact.Text),
                new SqlParameter("@AadharNumber", _TextBoxAadhar.Text),
                new SqlParameter("@PANNumber", _TextBoxPAN.Text),
                new SqlParameter("@VoterNumber", _TextBoxVoter.Text),
                new SqlParameter("@Facility", _DropDownListFacility.SelectedValue),
                new SqlParameter("@MonthlyRent", _TextBoxAmount.Text),
                new SqlParameter("@Advance", _TextAdvPayment.Text),
                new SqlParameter("@RentStart", _TextBoxStartDate.Text)
            );

            ShowMessage("Tenant submitted successfully!", true);

            _ButtonSubmit.Visible = false;
            _ButtonDocVeri.Visible = true;
            _ButtonAddMore.Visible = true;
        }

        protected void _ButtonDocVeri_Click(object sender, EventArgs e)
        {
            string tenantId = string.IsNullOrEmpty(Request.QueryString["ID"]) ? GetLastInsertedTenantID() : Request.QueryString["ID"];
            Response.Redirect("~/KYCDoc.aspx?TenantID=" + tenantId);
        }

        protected void BindRooms()
        {
            string query = _DropDownListType.SelectedValue == "Main Tenant"
                ? @"SELECT f.ID, f.Building+' '+f.Location+' - '+f.Title AS Title
                    FROM Facility f
                    WHERE NOT ID IN (SELECT Facility FROM Tenant WHERE Active=1 AND TenantType='Main Tenant')
                    ORDER BY ID"
                : @"SELECT DISTINCT f.ID, f.Building+' '+f.Location+' - '+f.Title AS Title
                    FROM Facility f
                    INNER JOIN Tenant t ON t.Facility=f.ID
                    WHERE t.Active=1 AND t.TenantType='Main Tenant'
                    ORDER BY f.ID";

            Utility._BindDropdown(_DropDownListFacility, query, "ID", "Title", true);
        }

        protected void _DropDownListType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRooms();
        }

        protected void _DropDownListFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_DropDownListType.SelectedValue == "Partner Tenant" && !string.IsNullOrEmpty(_DropDownListFacility.SelectedValue))
            {
                string query = $"SELECT RentStart, MonthlyRent, MeterReadingStart FROM Tenant WHERE TenantType='Main Tenant' AND Facility={_DropDownListFacility.SelectedValue}";
                DataTable dt = Utility._GetDataTable(query);

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
            BindRooms();
            _ButtonSubmit.Visible = true;
            _ButtonDocVeri.Visible = false;
            _ButtonAddMore.Visible = false;
            _LiteralMSG.Text = string.Empty;
        }
        private string GetLastInsertedTenantID()
        {
            DataTable dt = Utility._GetDataTable("SELECT TOP 1 ID FROM Tenant ORDER BY ID DESC");
            return dt.Rows.Count > 0 ? dt.Rows[0]["ID"].ToString() : "0";
        }

        private void ShowMessage(string message, bool success)
        {
            string cssClass = success ? "bg-success" : "bg-danger";
            _LiteralMSG.Text = $"<div class='p-3 mb-2 {cssClass} text-white'>{message}</div>";
        }
    }
}
