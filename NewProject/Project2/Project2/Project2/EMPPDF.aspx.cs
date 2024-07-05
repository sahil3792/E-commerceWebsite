using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Xml.Linq;

namespace Project2
{
    public partial class EMPPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the userID from session
                if (Session["UserID"] != null)
                {
                    string employeeID = Session["userID"].ToString();
                    DisplayEmployeeDetails(employeeID);
                }
                else
                {
                    // Handle if userID is not found in session
                    lblMessage.Text = "Employee ID not found in session.";
                }
            }
        }

        private void DisplayEmployeeDetails(string employeeID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            string query = @"
                SELECT Name, Email, Salary, PaidLeave, SickLeave, CasualLeave 
                FROM Users 
                WHERE UserID = @EmployeeID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                        conn.Open();

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string employeeName = reader["Name"].ToString();
                            decimal salary = Convert.ToDecimal(reader["Salary"]);
                            int paidLeaves = Convert.ToInt32(reader["PaidLeave"]);
                            int sickLeaves = Convert.ToInt32(reader["SickLeave"]);
                            int casualLeaves = Convert.ToInt32(reader["CasualLeave"]);

                            // Calculate total leaves excluding weekends
                            

                            // Calculate daily salary based on working days
                            int workingDays = GetWorkingDaysInMonth();
                            Response.Write($"<script>alert({workingDays})</script>");
                            decimal dailySalary = salary / workingDays;

                            // Calculate leaves taken and leaves left
                            int leavesTakenPaid = 12 - paidLeaves;
                            int leavesTakenSick = 8 - sickLeaves;
                            int leavesTakenCasual = 4 - casualLeaves;
                            int totalLeaves = leavesTakenCasual+leavesTakenSick;
                            int leavesLeftPaid = paidLeaves;
                            int leavesLeftSick = sickLeaves;
                            int leavesLeftCasual = casualLeaves;
                            workingDays = workingDays-totalLeaves;
                            // Calculate total salary based on leave type
                            decimal totalSalary = 0;
                            totalSalary= dailySalary *workingDays;

                            // Display employee details on the UI
                            lblEmployeeName.Text = $"Employee Name: {employeeName}";
                            lblEmployeeID.Text = $"Employee ID: {employeeID}";
                            lblEmployeeEmail.Text = $"Email: {reader["Email"].ToString()}";
                            lblEmployeeSalary.Text = $"Salary: {salary:C}";
                            lblCalculatedSalary.Text = $"Calculated Salary: {totalSalary:C}";
                            lblPaidLeavesTaken.Text = $"Paid Leaves Taken: {leavesTakenPaid}";
                            lblCasualLeavesTaken.Text = $"Casual Leaves Taken: {leavesTakenCasual}";
                            lblSickLeavesTaken.Text = $"Sick Leaves Taken: {leavesTakenSick}";
                            lblPaidLeavesLeft.Text = $"Paid Leaves Left: {leavesLeftPaid}";
                            lblCasualLeavesLeft.Text = $"Casual Leaves Left: {leavesLeftCasual}";
                            lblSickLeavesLeft.Text = $"Sick Leaves Left: {leavesLeftSick}";
                        }
                        else
                        {
                            lblMessage.Text = "Employee details not found.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                lblMessage.Text = $"Error: {ex.Message}";
            }
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
                DateTime date = new DateTime(today.Year, today.Month, day);
                
                    workingDays++;
                
            }
            return workingDays;
        }

        protected void btnDownloadPDF_Click(object sender, EventArgs e)
        {
            Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
            MemoryStream ms = new MemoryStream();
            PdfWriter.GetInstance(pdfDoc, ms);
            pdfDoc.Open();

            // Add employee details to the PDF
            pdfDoc.Add(new iTextSharp.text.Paragraph("Employee Details"));
            pdfDoc.Add(new iTextSharp.text.Paragraph("" + lblEmployeeName.Text));
            pdfDoc.Add(new iTextSharp.text.Paragraph(" " + lblEmployeeID.Text));
            pdfDoc.Add(new iTextSharp.text.Paragraph("" + lblEmployeeEmail.Text));
            pdfDoc.Add(new iTextSharp.text.Paragraph("" + lblEmployeeSalary.Text));
            pdfDoc.Add(new iTextSharp.text.Paragraph("" + lblCalculatedSalary.Text));
            pdfDoc.Add(new iTextSharp.text.Paragraph("" + lblPaidLeavesTaken.Text));
            pdfDoc.Add(new iTextSharp.text.Paragraph("" + lblCasualLeavesTaken.Text));
            pdfDoc.Add(new iTextSharp.text.Paragraph("" + lblSickLeavesTaken.Text));
            pdfDoc.Add(new iTextSharp.text.Paragraph("" + lblPaidLeavesLeft.Text));
            pdfDoc.Add(new iTextSharp.text.Paragraph("" + lblCasualLeavesLeft.Text));
            pdfDoc.Add(new iTextSharp.text.Paragraph("" + lblSickLeavesLeft.Text));

            pdfDoc.Close();

            byte[] fileBytes = ms.ToArray();
            ms.Close();

            // Send the PDF file to the browser
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=EmployeeDetails.pdf");
            Response.ContentType = "application/pdf";
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(fileBytes);
            Response.End();
        }
    }
}