using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FruitTablesWebsite
{
    public partial class AdminProductList : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {

            string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            conn = new SqlConnection(cs);
            conn.Open();
            if (!IsPostBack)
            {
                BindProductGrid();
            }
        }
        protected void ProductGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Handle deletion logic
            int productId = Convert.ToInt32(ProductGridView.DataKeys[e.RowIndex].Value);
            DeleteProduct(productId);
            BindProductGrid(); // Rebind GridView after deletion
        }

        

        
        private void BindProductGrid()
        {
            string query = "exec DisplayProduct";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {
                DataTable dt = new DataTable();
                dt.Load(rdr); // Load data into DataTable

                ProductGridView.DataSource = dt;
                ProductGridView.DataBind();
            }
            else
            {
                // Handle case where no data is returned
                ProductGridView.DataSource = null;
                ProductGridView.DataBind();
            }
        }


        private void DeleteProduct(int productId)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();

                    // Delete from Cart table first (if there are related entries)
                    string deleteFromCartQuery = "DELETE FROM Cart WHERE ProductID = @ProductId";
                    using (SqlCommand cmdCart = new SqlCommand(deleteFromCartQuery, conn))
                    {
                        cmdCart.Parameters.AddWithValue("@ProductId", productId);
                        cmdCart.ExecuteNonQuery();
                    }

                    // Now delete from Products table
                    string deleteFromProductsQuery = "DELETE FROM Product WHERE Id = @ProductId";
                    using (SqlCommand cmdProducts = new SqlCommand(deleteFromProductsQuery, conn))
                    {
                        cmdProducts.Parameters.AddWithValue("@ProductId", productId);
                        cmdProducts.ExecuteNonQuery();
                    }

                    // Rebind the GridView or update any other UI as needed
                    BindProductGrid();
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions or log errors
                Console.WriteLine("Error deleting product: " + ex.Message);
                // Optionally, throw or handle the exception in a way appropriate for your application
            }
        }
    }
}