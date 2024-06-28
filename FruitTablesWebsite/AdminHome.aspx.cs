using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
namespace FruitTablesWebsite
{
    public partial class AdminHome : System.Web.UI.Page
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
            // Retrieve form inputs
            string name = TextBoxProductName.Text;
            string details = TextBoxProductDetails.Text;
            string category = TextBoxCategory.Text;
            string origin = TextBoxOrigin.Text;
            string check = TextBoxCheck.Text;
            string quality = TextBoxQuality.Text;

            decimal price = decimal.Parse(TextBoxPrice.Text);
            decimal quantity = decimal.Parse(TextBoxQuantity.Text);

            // Handle file upload
            if (FileUploadImage.HasFile)
            {
                string fileName = Path.GetFileName(FileUploadImage.FileName);
                string imagePath = "UploadedImage/" + fileName;
                string uploadFolderPath = Server.MapPath("UploadedImage/");

                // Save file to server
                FileUploadImage.SaveAs(Path.Combine(uploadFolderPath, fileName));

                // SQL query with parameters
                string query = "EXEC InsertProduct @Name, @Details, @Price, @ImagePath, @Category, @Origin, @Check, @Quantity, @Quality";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Details", details);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@ImagePath", imagePath);
                    cmd.Parameters.AddWithValue("@Category", category);
                    cmd.Parameters.AddWithValue("@Origin", origin);
                    cmd.Parameters.AddWithValue("@Check", check);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@Quality", quality);

                    // Execute the query
                    
                    cmd.ExecuteNonQuery();
                    

                    // Display success message
                    Response.Write("<script>alert('Product added successfully')</script>");
                }
            }
            else
            {
                // Handle case where no file is uploaded
                // You may want to show an error message or take appropriate action
                Response.Write("<script>alert('Please upload an image file')</script>");
            }
        }
    }
}