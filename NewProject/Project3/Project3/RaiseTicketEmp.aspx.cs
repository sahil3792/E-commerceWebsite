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
    public partial class RaiseTicketEmp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropDownList();
            }
        }

        private void BindDropDownList()
        {
            string connString = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT UserID, Name FROM Users";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                DropDownList1.DataSource = dr;
                DropDownList1.DataTextField = "Name";
                DropDownList1.DataValueField = "UserId";
                DropDownList1.DataBind();
                conn.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string designation = TextBox1.Text;
            string raiseTicketTo = DropDownList1.SelectedValue;
            string ticketDescription = TextBox2.Text;
            string attachmentFileName = FileUpload1.HasFile ? FileUpload1.FileName : string.Empty;

            // Fetch UserId from session
            int userId = (int)Session["User"];
            string userName = GetUserNameById(userId);

            string connString = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "INSERT INTO Ticket (Designation, RaiseTicketTo, TicketDescription, AttachmentFileName, RaisedBy) VALUES (@Designation, @RaiseTicketTo, @TicketDescription, @AttachmentFileName, @RaisedBy)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Designation", designation);
                cmd.Parameters.AddWithValue("@RaiseTicketTo", raiseTicketTo);
                cmd.Parameters.AddWithValue("@TicketDescription", ticketDescription);
                cmd.Parameters.AddWithValue("@AttachmentFileName", attachmentFileName);
                cmd.Parameters.AddWithValue("@RaisedBy", userName);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            if (FileUpload1.HasFile)
            {
                string filePath = Server.MapPath("~/Attachments/") + FileUpload1.FileName;
                FileUpload1.SaveAs(filePath);
            }

            // Optionally, display a success message or redirect to a success page
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ticket raised successfully!');", true);
        }

        private string GetUserNameById(int userId)
        {
            string userName = string.Empty;
            string connString = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT Name FROM Users WHERE UserId = @UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    userName = dr["Name"].ToString();
                }
                conn.Close();
            }
            return userName;
        }
    }
}