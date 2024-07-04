using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project3
{
    public partial class JoiningFormHr : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            conn = new SqlConnection(cs);
            conn.Open();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string name = TextBox1.Text;
            string contact = TextBox2.Text;
            string email = TextBox3.Text;
            string salary = TextBox4.Text;
            DateTime dob = Calendar1.SelectedDate;
            DateTime doj = Calendar2.SelectedDate;
            int userId = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Name, Contact, Email, Salary, DOB, DOJ, Urole) VALUES (@Name, @Contact, @Email, @Salary, @DOB, @DOJ, @Urole);";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Contact", contact);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Salary", salary);
                    cmd.Parameters.AddWithValue("@DOB", dob);
                    cmd.Parameters.AddWithValue("@DOJ", doj);
                    cmd.Parameters.AddWithValue("@Urole", "EMP");

                    conn.Open();

                    cmd.ExecuteNonQuery();
                    
                }
            }

            // Optionally, display a success message or redirect to another page
            Response.Write("<script>alert('Record inserted successfully');</script>");
            SendEmail(email, userId, name);
        }
        private void SendEmail(string toEmail, int userId, string userName)
        {
            string fromEmail = "sahilharshalv@gmail.com"; // Replace with your email
            string subject = "Welcome to the Company!";
            string body = $"<p>Dear {userName},</p><p>Welcome to the company! Your User ID is: {userId}</p><p>Login to Portal using {userId} and {toEmail}</p><p>Best Regards,<br>HR Team</p>";

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true; // Set to true if the email body is in HTML

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"); // Replace with your SMTP server address
            smtpClient.Port = 587; // Replace with your SMTP server port
            smtpClient.Credentials = new System.Net.NetworkCredential("sahilharshalv@gmail.com", "gkrittwiezprqrbg"); // Replace with your email credentials
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mail);
                Response.Write("<script>alert('Email sent successfully');</script>");
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error sending email: {ex.Message}');</script>");
            }
        }
    }
}