using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maheshamv3
{
    public partial class MeterReading : System.Web.UI.Page
    {
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddMonths(-1), INDIAN_ZONE);
                _DropDownListMonth.SelectedValue = indianTime.ToString("MMM").ToUpper();
                _DropDownListYear.SelectedValue = indianTime.Year.ToString();
                BindFacilityDropdown();
            }
        }

        protected void BindFacilityDropdown()
        {
            _LiteralMSG.Text = "";
            bool showAll = _DropDownListFacility.SelectedValue == "1000";
            string query = $@"SELECT f.ID, f.Building + ' ' + f.Title + ' ' + f.Location AS Title FROM Facility f WHERE f.Active = 1 {(showAll ? "" : $"AND f.ID NOT IN (SELECT Facility FROM MeterReading WHERE Year={_DropDownListYear.SelectedValue} AND MonthNo={_DropDownListMonth.SelectedIndex + 1})")}ORDER BY ID";

            Utility._BindDropdown(_DropDownListFacility, query, "ID", "Title", true);

            if (!showAll)
                _DropDownListFacility.Items.Add(new ListItem("Show ALL", "1000"));
        }

        protected void _ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (_DropDownListFacility.SelectedIndex == 0)
            {
                _LiteralMSG.Text = "<div class='p-3 mb-2 bg-danger text-white'>Please select a facility or room.</div>";
                return;
            }

            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            string fileName = "";

            if (_FileUpload.HasFile)
            {
                string extension = System.IO.Path.GetExtension(_FileUpload.FileName);
                fileName = $"{_DropDownListFacility.SelectedValue}_{_DropDownListMonth.SelectedValue}{indianTime.Year}{(string.IsNullOrEmpty(extension) ? ".jpg" : extension)}";
                _FileUpload.SaveAs(Server.MapPath("~/MeterReading/" + fileName));
            }

            // 1️⃣ Insert/Update MeterReading
               Utility.ExecuteQuery(@"IF EXISTS (SELECT 1 FROM MeterReading WHERE Year=@Year  AND MonthNo=@MonthNo AND Facility=@Facility)
                UPDATE MeterReading SET Reading=@Reading,ReadingFile=@ReadingFile,ReadingOn=@ReadingOn, ReadingBy=@ReadingBy WHERE Year=@Year AND MonthNo=@MonthNo AND Facility=@Facility  
                ELSE INSERT INTO MeterReading(Facility, Reading, ReadingFile, ReadingOn, ReadingBy, Year, MonthNo, Active)VALUES(@Facility, @Reading, @ReadingFile, @ReadingOn, @ReadingBy, @Year, @MonthNo, 1');",
                false,
                new SqlParameter("@Facility", _DropDownListFacility.SelectedValue),
                new SqlParameter("@Reading", _TextBoxReading.Text),
                new SqlParameter("@ReadingFile", fileName),
                new SqlParameter("@ReadingOn", indianTime),
                new SqlParameter("@Year", _DropDownListYear.SelectedValue),
                new SqlParameter("@MonthNo", _DropDownListMonth.SelectedIndex + 1),
                new SqlParameter("@ReadingBy", "Shrikant")
            );

            // 2️⃣ Fetch tenants
            string tenantQuery = $@"SELECT ID, Facility, RentStart, MonthlyRent, MeterReadingStart FROM Tenant WHERE TenantType='Main Tenant' AND Active=1 {(_DropDownListFacility.SelectedIndex == 0 ? "" : $"AND Facility={_DropDownListFacility.SelectedValue}")}";

            DataTable tenants = Utility._GetDataTable(tenantQuery);

            foreach (DataRow dr in tenants.Rows)
            {
                int tenantId = Convert.ToInt32(dr["ID"]);
                int facilityId = Convert.ToInt32(dr["Facility"]);
                int monthlyRent = Convert.ToInt32(dr["MonthlyRent"]);
                DateTime rentStart = Convert.ToDateTime(dr["RentStart"]);

                // 3️⃣ Previous month meter reading
                DateTime prevMonth = new DateTime(Convert.ToInt32(_DropDownListYear.SelectedValue), _DropDownListMonth.SelectedIndex + 1, 1).AddMonths(-1);
                DataTable prevMeterDt = Utility._GetDataTable($@"SELECT TOP 1 Reading FROM MeterReading WHERE Facility={facilityId} AND Year={prevMonth.Year} AND MonthNo={prevMonth.Month} ORDER BY ID DESC");

                int meterStart = prevMeterDt.Rows.Count > 0 ? Convert.ToInt32(prevMeterDt.Rows[0][0]) : Convert.ToInt32(dr["MeterReadingStart"]);
                int meterEnd = Convert.ToInt32(_TextBoxReading.Text);
                meterEnd = meterStart >= meterEnd ? meterStart : meterEnd;

                DateTime periodStart = new DateTime(Convert.ToInt32(_DropDownListYear.SelectedValue), _DropDownListMonth.SelectedIndex + 1, rentStart.Day);
                DateTime periodEnd = periodStart.AddMonths(1).AddDays(-1);


                DataTable prevMeterDue = Utility._GetDataTable($@"SELECT due FROM rent WHERE AmountType='Rental' and Tenant={tenantId} AND rYear={prevMonth.Year.ToString()} AND rMonth='{prevMonth.ToString("MMM").ToUpper()}' AND Facility={facilityId}");
                int totalAmount = (monthlyRent + ((meterEnd - meterStart) > 0 ? (meterEnd - meterStart) * 7 : 0)) + (prevMeterDue.Rows.Count == 0 ? 0 : Convert.ToInt32(prevMeterDue.Rows[0][0]));

                // 4️⃣ Insert/Update Rent
                DataTable tenantRent = Utility._GetDataTable($@"
                    SELECT *
                    FROM Rent
                    WHERE AmountType='Rental' and Tenant={tenantId} AND rYear={_DropDownListYear.SelectedValue} AND rMonth='{_DropDownListMonth.SelectedValue}' AND Facility={facilityId}");

                if (tenantRent.Rows.Count > 0)
                {
                    Utility.ExecuteQuery(@"UPDATE Rent SET MeterStart=@MeterStart,MeterEnd=@MeterEnd,TotalAmount=@TotalAmount,due=@TotalAmount WHERE ID=@ID",
                        false,
                        new SqlParameter("@ID", tenantRent.Rows[0]["ID"]),
                        new SqlParameter("@MeterStart", meterStart),
                        new SqlParameter("@MeterEnd", meterEnd),
                        new SqlParameter("@TotalAmount", totalAmount)
                    );
                }
                else
                {
                    Utility.ExecuteQuery(@"
                        INSERT INTO Rent(Facility, Tenant, Amount, PeriodStart, PeriodEnd, MeterStart, MeterEnd, rMonth, rYear, rMonthNo, TotalAmount, Active,Due,AmountType)VALUES(@Facility, @Tenant, @Amount, @PeriodStart, @PeriodEnd, @MeterStart, @MeterEnd, @rMonth, @rYear, @rMonthNo, @TotalAmount, 1, @TotalAmount,'Rental')",
                        false,
                        new SqlParameter("@Facility", facilityId),
                        new SqlParameter("@Tenant", tenantId),
                        new SqlParameter("@Amount", monthlyRent),
                        new SqlParameter("@PeriodStart", periodStart),
                        new SqlParameter("@PeriodEnd", periodEnd),
                        new SqlParameter("@MeterStart", meterStart),
                        new SqlParameter("@MeterEnd", meterEnd),
                        new SqlParameter("@rMonth", _DropDownListMonth.SelectedValue),
                        new SqlParameter("@rYear", _DropDownListYear.SelectedValue),
                        new SqlParameter("@rMonthNo", _DropDownListMonth.SelectedIndex + 1),
                        new SqlParameter("@TotalAmount", totalAmount)
                    );
                }
            }

            _LiteralMSG.Text = "<div class='p-3 mb-2 bg-success text-white'>Meter Reading & Rent have been Submitted Successfully!</div>";
            _ButtonNewReading.Visible = true;
            _ButtonSubmit.Visible = false;
        }

        protected void _ButtonNewReading_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MeterReading.aspx");
        }

        protected void _DropDownListType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindFacilityDropdown();
        }

        protected void _DropDownListFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_DropDownListFacility.SelectedValue == "1000")
                BindFacilityDropdown();
        }
    }
}
