using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static FruitTablesWebsite.UserHome;
using System.Xml.Linq;

namespace FruitTablesWebsite
{
    public partial class UserHome : System.Web.UI.Page
    {
        SqlConnection conn; 
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            conn = new SqlConnection(cs);
            conn.Open();
            if (!IsPostBack)
            {
                BindProducts(null);
            }
        }
        protected void CategoryButton_Click(object sender, EventArgs e)
        {
            string category = ((Button)sender).CommandArgument;
            hdnCategory.Value = category; // Update hidden field with the selected category
            BindProducts(category);
        }
        private void BindProducts(string category)
        {
            
            List<Product> products = new List<Product>();
            string query;
            if (string.IsNullOrEmpty(category) || category == "All Products")
            {
                query = "SELECT TOP 8 Id, Name, Category, Description, Price, ImagePath FROM product"; 
            }
            else
            {
                query = "SELECT top 8 Id, Name, Category, Description, Price, ImagePath FROM product WHERE Category = @Category";
            }

            SqlCommand cmd = new SqlCommand(query, conn);

            if (!string.IsNullOrEmpty(category) && category != "All Products")
            {
                cmd.Parameters.AddWithValue("@Category", category);
            }

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Product product = new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Category = reader["Category"].ToString(),
                    Description = reader["Description"].ToString(),
                    Price = Convert.ToDecimal(reader["Price"]),
                    ImagePath = reader["ImagePath"].ToString()
                };
                products.Add(product);
            }
            rptProducts.DataSource = products;
            rptProducts.DataBind();
        }

        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string ImagePath { get; set; }
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
    }

}