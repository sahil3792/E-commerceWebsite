using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project2
{
    public partial class EmpApplyLeave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Read the values from the form controls
            int employeeID = int.Parse(Session["UserID"].ToString());
            string leaveType = ddlLeaveType.SelectedValue;
            string startDate = txtStartDate.Text;
            string endDate = txtEndDate.Text;
            string reason = txtReason.Text;

            // Save the leave details to the database
            SaveLeaveDetails(employeeID, leaveType, startDate, endDate, reason);

            // Display a message or redirect to another page
            Response.Write("<script>alert('Leave application submitted successfully.')</script>");
            
        }

        private void SaveLeaveDetails(int employeeID, string leaveType, string startDate, string endDate, string reason)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string insertQuery = "INSERT INTO LeaveApplications (EmployeeID, LeaveType, StartDate, EndDate, Reason) VALUES (@EmployeeID, @LeaveType, @StartDate, @EndDate, @Reason)";
                string updateLeaveQuery = "";
                string updateTotalLeavesQuery = "";

                int leaveDays = CalculateLeaveDays(startDate, endDate); // Calculate leave days excluding weekends

                // Determine which leave type to update and update query
                switch (leaveType)
                {
                    case "Paid Leave":
                        updateLeaveQuery = "UPDATE Users SET PaidLeave = PaidLeave - @LeaveDays WHERE UserID = @EmployeeID";
                        break;
                    case "Sick Leave":
                        updateLeaveQuery = "UPDATE Users SET SickLeave = SickLeave - @LeaveDays WHERE UserID = @EmployeeID";
                        break;
                    case "Casual Leave":
                        updateLeaveQuery = "UPDATE Users SET CasualLeave = CasualLeave - @LeaveDays WHERE UserID = @EmployeeID";
                        break;
                    default:
                        // Handle default case or error scenario
                        break;
                }

                // Update TotalLeaves query
                updateTotalLeavesQuery = "UPDATE Users SET TotalLeaves = PaidLeave + SickLeave + CasualLeave WHERE UserID = @EmployeeID";

                // Execute the insert query for LeaveApplications
                using (SqlCommand cmdInsert = new SqlCommand(insertQuery, conn))
                {
                    cmdInsert.Parameters.AddWithValue("@EmployeeID", employeeID);
                    cmdInsert.Parameters.AddWithValue("@LeaveType", leaveType);
                    cmdInsert.Parameters.AddWithValue("@StartDate", startDate);
                    cmdInsert.Parameters.AddWithValue("@EndDate", endDate);
                    cmdInsert.Parameters.AddWithValue("@Reason", reason);

                    conn.Open();
                    cmdInsert.ExecuteNonQuery();
                }

                // Execute the update query for the specific leave type in Users table
                if (!string.IsNullOrEmpty(updateLeaveQuery))
                {
                    using (SqlCommand cmdUpdateLeave = new SqlCommand(updateLeaveQuery, conn))
                    {
                        cmdUpdateLeave.Parameters.AddWithValue("@EmployeeID", employeeID);
                        cmdUpdateLeave.Parameters.AddWithValue("@LeaveDays", leaveDays);
                        cmdUpdateLeave.ExecuteNonQuery();
                    }
                }

                // Execute the update query for TotalLeaves in Users table
                if (!string.IsNullOrEmpty(updateTotalLeavesQuery))
                {
                    using (SqlCommand cmdUpdateTotalLeaves = new SqlCommand(updateTotalLeavesQuery, conn))
                    {
                        cmdUpdateTotalLeaves.Parameters.AddWithValue("@EmployeeID", employeeID);
                        cmdUpdateTotalLeaves.ExecuteNonQuery();
                    }
                }
            }
        }

        private int CalculateLeaveDays(string startDate, string endDate)
        {
            DateTime start = DateTime.Parse(startDate);
            DateTime end = DateTime.Parse(endDate);

            int leaveDays = 0;
            for (DateTime date = start; date <= end; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    leaveDays++;
                }
            }

            return leaveDays;
        }
    }
}