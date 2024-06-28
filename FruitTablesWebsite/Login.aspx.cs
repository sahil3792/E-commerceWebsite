using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace FruitTablesWebsite
{
    public partial class Login : System.Web.UI.Page
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
            Response.Redirect("~/Register.aspx");
        }

        

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            string Username= TextBoxUsernameLogin.Text, Password= TextBoxPasswordLogin.Text;
            string query = $"exec CheckUser '{Username}','{Password}' ";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    if (rdr["Username"].Equals(Username) && rdr["Password"].Equals(Password))
                    {
                        if (rdr["UserRole"].Equals("Admin"))
                        {
                            Session["User"] = rdr["ID"];
                            Response.Redirect("~/AdminHome.aspx");
                        }
                        else
                        {
                            Session["User"] = rdr["ID"];
                            Response.Redirect("~/UserHome.aspx");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Incorrect Username or Password')</script>");
                    }
                }
            }
        }
    }
}