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
    public partial class ViewSolutionEmp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTicketData();
            }
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

        private void BindTicketData()
        {
            int userId = (int)Session["User"];
            string userName = GetUserNameById(userId);

            string connString = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT TicketID, Designation, TicketDescription, Solution, SolutionDate FROM Ticket WHERE RaisedBy = @UserId AND Solution IS NOT NULL";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userName);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
    }
}