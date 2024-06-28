using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace FruitTablesWebsite
{
    public partial class Register : System.Web.UI.Page
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
            Response.Redirect("~/Login.aspx");
        }

        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
            string username = TextBoxUsernameRegister.Text, email = TextBoxEmailRegister.Text, password = TextBoxPasswordRegister.Text,confirm_password = TextBoxConfirmPassRegister.Text,UserRole = "User";
            if (password != confirm_password)
            {
                Response.Write("<script>alert('passwords don't match')</script>");
            }
            string query = $"exec InsertUser '{username}','{email}','{password}','{UserRole}'";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('Successfully Registered')</script>");
            Response.Write("<script>alert('Please Login to Visit Fruitables')</script>");
            Response.Redirect("~/Login.aspx");
            Response.Write("<script>alert('Please Login to Visit Fruitables')</script>");
        }
    }
}