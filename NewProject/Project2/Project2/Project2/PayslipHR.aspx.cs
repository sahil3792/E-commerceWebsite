using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project2
{
    public partial class PayslipHR : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEmployees();
            }
        }

        private void BindEmployees()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            string query = @"
                SELECT 
                    UserID AS EmployeeID, 
                    Name, 
                    Email, 
                    Salary, 
                    dob, 
                    doj, 
                    Contact, 
                    PaidLeave, 
                    SickLeave, 
                    CasualLeave,
                    TotalLeaves 
                FROM Users 
                WHERE Urole = 'EMP'";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        gvEmployees.DataSource = dt;
                        gvEmployees.DataBind();
                    }
                }
            }
        }

        protected void gvEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CalculateSalary")
            {
                string employeeID = e.CommandArgument.ToString();
                CalculateSalary(employeeID);
            }
        }

        private void CalculateSalary(string employeeID)
        {
            decimal dailySalary = 0;
            string employeeName = "";
            int totalLeaves = GetTotalLeaves(employeeID);

            // Fetch the employee details from the database
            string connectionString = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            string query = @"
                SELECT Name, Salary, PaidLeave, SickLeave, CasualLeave 
                FROM Users 
                WHERE UserID = @EmployeeID";

            int paidLeaves = 0;
            int sickLeaves = 0;
            int casualLeaves = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        employeeName = reader["Name"].ToString();
                        dailySalary = Convert.ToDecimal(reader["Salary"]) / GetWorkingDaysInMonth(); // Calculate daily salary based on working days
                        paidLeaves = Convert.ToInt32(reader["PaidLeave"]);
                        sickLeaves = Convert.ToInt32(reader["SickLeave"]);
                        casualLeaves = Convert.ToInt32(reader["CasualLeave"]);
                    }
                }
            }
            int paidleavestaken = 12-paidLeaves;
            int sickleavestaken = 8-sickLeaves;
            int casualleavestaken = 4-casualLeaves;
            // Determine total leave days excluding weekends
            int workingDays = GetWorkingDaysInMonth() - sickleavestaken - casualleavestaken;
            Response.Write($"<script>alert('this is subtracted by sick leave and casual leave {workingDays}')</script>");

            // Calculate total salary based on leave type
            decimal totalSalary = 0;
            if (totalLeaves <= paidLeaves)
            {
                // No deduction for paid leaves
                totalSalary = workingDays * dailySalary;
            }
            else if (totalLeaves <= paidLeaves + sickLeaves)
            {
                // Deduct salary for sick leaves
                int sickLeaveDays = totalLeaves - paidLeaves;
                int unpaidLeaves = sickLeaveDays;
                workingDays -= unpaidLeaves;
                totalSalary = workingDays * dailySalary;
            }
            else if (totalLeaves <= paidLeaves + sickLeaves + casualLeaves)
            {
                // Deduct salary for casual leaves
                int casualLeaveDays = totalLeaves - paidLeaves - sickLeaves;
                int unpaidLeaves = casualLeaveDays;
                workingDays -= unpaidLeaves;
                totalSalary = workingDays * dailySalary;
            }
            else
            {
                // No salary for leaves beyond allocated leaves
                totalSalary = 0;
            }

            lblMessage.Text = $"Total Salary for {employeeName} (Employee ID: {employeeID}): {totalSalary:C}";
        }

        private int GetTotalLeaves(string employeeID)
        {
            int totalLeaves = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            string query = @"
                SELECT COUNT(*) 
                FROM LeaveApplications 
                WHERE EmployeeID = @EmployeeID 
                AND DATEPART(dw, StartDate) NOT IN (1, 7)"; // Exclude Sundays (1) and Saturdays (7)

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                    conn.Open();
                    totalLeaves = (int)cmd.ExecuteScalar();
                }
            }

            return totalLeaves;
        }

        private int GetWorkingDaysInMonth()
        {
            DateTime today = DateTime.Today;
            int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
            int workingDays = 0;

            for (int day = 1; day <= daysInMonth; day++)
            {
                
                    workingDays++;
                    
                
            }

            Response.Write($"<script>alert({workingDays})</script>");
            return workingDays;
        }
    }
}
