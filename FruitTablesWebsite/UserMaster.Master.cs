using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FruitTablesWebsite
{
    public partial class UserMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            if (Session["User"]!=null)
            {

            }
        }

        protected void ButtonUserProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserProfile.aspx");
        }

        protected int FetchCartCountForUser()
        {
            int cartCount = 0;
            // Implement your logic to fetch cart count from database
            // Example: Query your database to count items in the cart for the given userId
            // Replace this with your actual database query
            string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            SqlConnection conn = new SqlConnection(cs);
            string query = $"SELECT COUNT(*) FROM Cart WHERE UserId = '{int.Parse(Session["User"].ToString())}'";
            SqlCommand command = new SqlCommand(query, conn);
            
            conn.Open();
            cartCount = (int)command.ExecuteScalar(); // ExecuteScalar returns the first column of the first row
            return cartCount;
            
        }
    }
}