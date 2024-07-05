using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace Project2
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            int UserId = int.Parse(TextBox1.Text);
              string   EmailID = TextBox2.Text;
            string query = "Select * from Users";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    if (rdr["UserID"].Equals(UserId) && rdr["Email"].Equals(EmailID))
                    {
                        if (rdr["Urole"].Equals("HR"))
                        {
                            Session["UserID"] = rdr["UserID"];
                            Response.Redirect("/HrJoiningForm.aspx");
                        }
                        else
                        {
                            Session["UserID"] = rdr["UserID"];
                            Response.Redirect("/EmpApplyLeave.aspx");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid Credentials ')</script>");
                    }
                }
            }

        }
    }
}