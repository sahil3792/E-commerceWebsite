using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project2
{
    public partial class HRMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonLogout_Click(object sender, EventArgs e)
        {
            // Clear session variables
            Session.Clear();
            Session.Abandon(); // Abandon the session

            // Redirect to the login page or any other page
            Response.Redirect("~/Login.aspx"); // Replace with your login page URL
        }
    }
}