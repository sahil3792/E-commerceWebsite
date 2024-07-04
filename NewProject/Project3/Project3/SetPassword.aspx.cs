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
    public partial class SetPassword : System.Web.UI.Page
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
            string emailID = TextBox1.Text, password = TextBox2.Text, confirmpassword =TextBox3.Text;
            string query = $"Select * from Users where Email = '{emailID}'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                while(reader.HasRows)
                {
                    if (password != confirmpassword)
                    {
                        Response.Write("<script>alert('passwords dont match')</script>");
                    }
                    else
                    {
                        string q = $"Update users set password = '{password}' where Email = '{emailID}';";
                        SqlCommand sqlCommand = new SqlCommand(q, conn);
                        sqlCommand.ExecuteNonQuery();
                        Response.Write("<script>alert('Please Login ')</script>");
                        
                    }
                    
                }
                Response.Redirect("LoginForm.aspx");
            }

            
        }
    }
}