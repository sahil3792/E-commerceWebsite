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
    }
}