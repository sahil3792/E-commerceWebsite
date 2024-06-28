using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Security.Cryptography;

namespace FruitTablesWebsite
{
    public partial class UserCheckOut : System.Web.UI.Page
    {
        SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            conn =new SqlConnection(cs);
            conn.Open();
            if (!IsPostBack)
            {
                BindCheckoutCart();
                CalculateTotal();
            }
        }

        private void BindCheckoutCart()
        {
            string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = $"exec DisplayCart {int.Parse(Session["User"].ToString())}";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }

            rptCheckoutCart.DataSource = dt;
            rptCheckoutCart.DataBind();
        }

        private void CalculateTotal()
        {
            DataTable dt = (DataTable)rptCheckoutCart.DataSource;

            decimal subtotal = 0;
            foreach (DataRow row in dt.Rows)
            {
                subtotal += Convert.ToDecimal(row["Price"]) * Convert.ToInt32(row["Quantity"]);
            }

            decimal shipping = 30; // Flat rate shipping
            decimal total = subtotal + shipping;

            // Bind subtotal and total to labels
            lblSubtotal.Text = subtotal.ToString("F2");
            lblTotal.Text = total.ToString("F2");
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            // Your order placement logic here
        }
    }
}