using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project3
{
    public partial class GeneratePayslipHR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            string connString = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT UserId, Name, Email, Salary, DOB, DOJ, Contact FROM Users WHERE Urole = 'EMP'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
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
            
            // Determine total leave days excluding weekends
            int workingDays = GetWorkingDaysInMonth();
            if (paidLeaves<0)
            {
                workingDays = workingDays + paidLeaves;
            }
            if (sickLeaves<0)
            {
                workingDays += sickLeaves;
            }
            if(casualLeaves<0)
            {
                workingDays += casualLeaves;
            }
            Response.Write($"<script>alert('this is subtracted by sick leave and casual leave {workingDays}')</script>");

            // Calculate total salary based on leave type
            decimal totalSalary = 0;
            totalSalary = workingDays * dailySalary;
            //if (totalLeaves <= paidLeaves)
            //{
            //    // No deduction for paid leaves
            //    totalSalary = workingDays * dailySalary;
            //}
            //else if (totalLeaves <= paidLeaves + sickLeaves)
            //{
            //    // Deduct salary for sick leaves
            //    int sickLeaveDays = totalLeaves - paidLeaves;
            //    int unpaidLeaves = sickLeaveDays;
            //    workingDays -= unpaidLeaves;
            //    totalSalary = workingDays * dailySalary;
            //}
            //else if (totalLeaves <= paidLeaves + sickLeaves + casualLeaves)
            //{
            //    // Deduct salary for casual leaves
            //    int casualLeaveDays = totalLeaves - paidLeaves - sickLeaves;
            //    int unpaidLeaves = casualLeaveDays;
            //    workingDays -= unpaidLeaves;
            //    totalSalary = workingDays * dailySalary;
            //}
            //else
            //{
            //    // No salary for leaves beyond allocated leaves
            //    totalSalary = 0;
            //}

            lblMessage.Text = $"Total Salary for {employeeName} (Employee ID: {employeeID}): {totalSalary:C}";
        }

        private int GetTotalLeaves(string employeeID)
        {
            int totalLeaves = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            string query = $"select * from Users where UserID = '{employeeID}'"; // Exclude Sundays (1) and Saturdays (7)

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