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
    public partial class UserShopDetails : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            conn= new SqlConnection(cs);
            conn.Open();
            if (!IsPostBack)
            {
                if (Session["ProductId"] != null)
                {
                    int productId = Convert.ToInt32(Session["ProductId"]);
                    LoadProductDetails(productId);
                }
                else
                {
                    Response.Redirect("UserShop.aspx");
                }
            }
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

        private void LoadProductDetails(int productId)
        {
            string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT Name, Description, Price, ImagePath, Category,QuantityInKg, Country_Of_Origin, Quality, product_check FROM Product WHERE Id = @ProductId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblProductName.Text = reader["Name"].ToString();
                        lblCategory.Text = reader["Category"].ToString();
                        lblProductPrice.Text = "$" + reader["Price"].ToString() + " / kg";
                        imgProduct.ImageUrl = reader["ImagePath"].ToString();
                        lblProductDescription1.Text = reader["Description"].ToString(); // First description
                        lblProductDescription2.Text = ""; // Add additional descriptions if available
                        lblProductDescription3.Text = ""; // Add additional descriptions if available
                        lblProductDescription4.Text = ""; // Add additional descriptions if available
                        lblWeight.Text = reader["QuantityInKg"].ToString();
                        lblCountryOfOrigin.Text = reader["Country_Of_Origin"].ToString();
                        lblQuality.Text = reader["Quality"].ToString();
                        lblCheck.Text = reader["product_check"].ToString();
                        lblMinWeight.Text = reader["QuantityInKg"].ToString();
                    }
                }
            }
        }

    }
}