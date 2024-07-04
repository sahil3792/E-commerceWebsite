using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Project3
{
    public partial class ViewTicketEmp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTicketData();
            }
        }

        private void BindTicketData()
        {
            int userId = (int)Session["User"];

            string connString = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT TicketID, Designation, TicketDescription, AttachmentFileName, RaisedBy, CreatedAt FROM Ticket WHERE RaiseTicketTo = @UserId AND Solution IS NULL";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                string fileName = e.CommandArgument.ToString();
                string filePath = Server.MapPath("~/Attachments/") + fileName;

                if (File.Exists(filePath))
                {
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.WriteFile(filePath);
                    Response.End();
                }
                else
                {
                    // Handle file not found scenario
                    Response.Write("<script>alert('File not found.');</script>");
                }
            }
            else if (e.CommandName == "Solution")
            {
                string ticketID = e.CommandArgument.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", "showModal('" + ticketID + "');", true);
            }
        }

        protected void SubmitSolutionButton_Click(object sender, EventArgs e)
        {
            string solution = SolutionTextBox.Text;
            int ticketID = Convert.ToInt32(HiddenTicketID.Value);
            DateTime solutionDate = DateTime.Now;

            string connString = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "UPDATE Ticket SET Solution = @Solution, SolutionDate = @SolutionDate WHERE TicketID = @TicketID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Solution", solution);
                cmd.Parameters.AddWithValue("@SolutionDate", solutionDate);
                cmd.Parameters.AddWithValue("@TicketID", ticketID);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            // Rebind the data to refresh the grid and hide the modal
            BindTicketData();
            ScriptManager.RegisterStartupScript(this, GetType(), "HideModal", "$('#solutionModal').modal('hide');", true);
        }
    }
}