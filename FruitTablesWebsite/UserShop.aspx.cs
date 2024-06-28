using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FruitTablesWebsite
{
    public partial class UserShop : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            conn = new SqlConnection(cs);   
            conn.Open();
            if (!IsPostBack)
            {
                BindProducts();
            }
        }

        private void BindProducts()
        {
            string query = "exec DisplayProduct";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            rptProducts.DataSource = dt;
            rptProducts.DataBind();
            
        }

        protected void btnAddToCart_Command(object sender, CommandEventArgs e)
        {
            // Retrieve the ProductId from the CommandArgument
            int productId = Convert.ToInt32(e.CommandArgument);

            String UserID = Session["User"].ToString();
            Response.Write($"<script>alert('{productId}  {UserID}')</script>");
            string query = $"exec InsertInCart '{UserID}','{productId}'";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            // Add the product to the cart (this logic will depend on your specific implementation)
            //AddProductToCart(productId);

            // Optionally, you can redirect the user to another page or update the UI accordingly
            //Response.Redirect("Cart.aspx");
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            conn = new SqlConnection(cs);
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            sb.Append("SELECT Id, Name, Description, Price, ImagePath, Category FROM Product WHERE 1=1");

            // Search by keyword
            if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                sb.Append(" AND Name LIKE @Keywords");
                cmd.Parameters.AddWithValue("@Keywords", "%" + txtSearch.Text.Trim() + "%");
            }

            cmd.CommandText = sb.ToString();
            cmd.Connection = conn;
            

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            rptProducts.DataSource = dt;
            rptProducts.DataBind(); // Call the method to bind products based on search criteria
        }
        protected int GetProductCount(string productCheckValue)
        {
            int count = 0;
            string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT COUNT(*) FROM Product WHERE Product_check = @ProductCheck";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProductCheck", productCheckValue);

                con.Open();
                count = (int)cmd.ExecuteScalar();
            }

            return 5;
        }

        protected void FilterByCategory_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = (LinkButton)sender;
            string category = clickedButton.Text; // Get the category name from the clicked LinkButton

            // Perform the filtering logic based on the selected category
            BindProducts(); // Call the method to bind products based on selected category
        }
        protected void btnViewDetails_Command(object sender, CommandEventArgs e)
        {
            int productId = Convert.ToInt32(e.CommandArgument);
            string userID = Session["User"].ToString();
            Session["ProductId"] = productId;
            Response.Redirect("UserShopDetails.aspx");
        }

    }
}