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
    public partial class LoginForm : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            conn = new SqlConnection(cs);
            conn.Open();
        }

        

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SetPassword.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string email = TextBox1.Text, password= TextBox2.Text;
            string query = "select * from Users;";
            string Valid = "HR";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    if (rdr["Email"].Equals(email) && rdr["Password"].Equals(password))
                    {
                        if (rdr["Urole"].Equals(Valid))
                        {
                            Session["User"] = rdr["UserID"];
                            Response.Redirect("/HrHome.aspx");
                        }
                        else
                        {
                            Session["User"] = rdr["UserID"];
                            Response.Redirect("/EmpHome.aspx");
                        }
                    }
                    
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Credentials')</script>");
            }

        }
    }
}