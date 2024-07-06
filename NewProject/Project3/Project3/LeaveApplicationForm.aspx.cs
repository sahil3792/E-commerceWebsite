using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project3
{
    public partial class LeaveApplicationForm : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
       
            int employeeID = int.Parse(Session["User"].ToString());
            string leaveType = DropDownList1.SelectedValue;
            string startDate = txtStartDate.Text;
            string endDate = txtEndDate.Text;
            string reason = TextBox2.Text;

      
            SaveLeaveDetails(employeeID, leaveType, startDate, endDate, reason);

        
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

                int leaveDays = CalculateLeaveDays(startDate, endDate); 
                Response.Write($"<script>alert('{leaveDays}')</script>");
                
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
                        
                        break;
                }

                
                updateTotalLeavesQuery = "UPDATE Users SET TotalLeaves = PaidLeave + SickLeave + CasualLeave WHERE UserID = @EmployeeID";

                
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

                
                if (!string.IsNullOrEmpty(updateLeaveQuery))
                {
                    using (SqlCommand cmdUpdateLeave = new SqlCommand(updateLeaveQuery, conn))
                    {
                        cmdUpdateLeave.Parameters.AddWithValue("@EmployeeID", employeeID);
                        cmdUpdateLeave.Parameters.AddWithValue("@LeaveDays", leaveDays);
                        cmdUpdateLeave.ExecuteNonQuery();
                    }
                }

                
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