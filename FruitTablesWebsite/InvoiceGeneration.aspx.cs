using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace FruitTablesWebsite
{
    public partial class InvoiceGeneration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //// Generate and save the invoice PDF
            //string invoiceFilePath = GenerateAndSaveInvoice();

            //// Email the invoice PDF to the user
            //string userEmail = "user@example.com"; // Replace with the user's email address
            //EmailInvoice(userEmail, invoiceFilePath);
            if (!IsPostBack)
            {
                // Fetch order details based on order ID passed via query string or form data
                int orderID = int.Parse(Request.Form["order_id"]); // Assuming you get this ID from Razorpay or another source

                int orderdetailsId = StorePaymentDetails(orderID);
                // Fetch order details from your database
                OrderDetail orderDetail = FetchOrderDetails(orderdetailsId);

                if (orderDetail != null)
                {
                    // Fetch user details based on UserID in order details
                    User user = FetchUserDetails(orderDetail.UserID);

                    // Fetch product details from cart based on UserID
                    List<Product> products = FetchProductsInCart(orderDetail.UserID);

                    // Calculate total amount from products or order details
                    decimal totalAmount = CalculateTotalAmount(products);

                    // Display fetched data in the invoice
                    InvoiceNumber.InnerText = orderDetail.OrderDetailID.ToString();
                    InvoiceDate.InnerText = DateTime.Now.ToString("dd MMM yyyy");
                    Username.InnerText = user.Username;
                    UserAddress.InnerText = user.Address;

                    // Bind products to the repeater or similar control for listing
                    ProductsRepeater.DataSource = products;
                    ProductsRepeater.DataBind();

                    TotalAmount.InnerText = totalAmount.ToString("0.00");
                }
                else
                {
                    Response.Write("Order not found.");
                }
            }
        }

        private string GenerateAndSaveInvoice()
        {
            // Logic to generate and save the PDF invoice
            // Replace with your actual logic to generate the invoice PDF
            string invoiceFileName = "invoice_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
            string invoiceFilePath = Server.MapPath("~/Invoices/") + invoiceFileName;

            // Example: Generating a simple PDF using iTextSharp (you can use other libraries as well)
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(invoiceFilePath, FileMode.Create));
            document.Open();
            document.Add(new iTextSharp.text.Paragraph("Invoice for Your Purchase"));
            document.Close();

            return invoiceFilePath;
        }

        // Method to send email with invoice attachment
        private void EmailInvoice(string userEmail, string invoiceFilePath)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.example.com");

                mail.From = new MailAddress("your_email@example.com");
                mail.To.Add(userEmail);
                mail.Subject = "Invoice for Your Purchase";
                mail.Body = "Please find attached the invoice for your recent purchase.";

                Attachment attachment = new Attachment(invoiceFilePath);
                mail.Attachments.Add(attachment);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new NetworkCredential("username", "password");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                // Clean up resources after sending email
                attachment.Dispose();
                File.Delete(invoiceFilePath); // Optionally delete the PDF file after sending

                Response.Write("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Response.Write("Failed to send email. Error: " + ex.Message);
            }
        }
        private int StorePaymentDetails(int orderID)
        {
            string paymentId = Request.Form["razorpay_payment_id"];
            string orderId = Request.Form["razorpay_order_id"];
            string signature = Request.Form["razorpay_signature"];
            decimal amount = decimal.Parse(Request.Form["amount"]);
            string currency = Request.Form["currency"];
            string name = Request.Form["name"];
            string contact = Request.Form["contact"];
            string email = Request.Form["email"];

            // Example: Store payment details in OrderDetails table
            string connectionString = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            string query = @"INSERT INTO OrderDetails (PaymentId, OrderId, Signature, Amount,Userid,Uname, Contact, Email, dateOfOrder)
                    VALUES (@PaymentId, @OrderId, @Signature, @Amount,@userid, @Uname, @Contact, @Email, @dateOfOrder);SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@PaymentId", paymentId);
                command.Parameters.AddWithValue("@OrderId", orderID);
                command.Parameters.AddWithValue("@Signature", signature);
                command.Parameters.AddWithValue("@Amount", amount);
                command.Parameters.AddWithValue("@userid", int.Parse(Session["User"].ToString()));
                command.Parameters.AddWithValue("@UName", name);
                command.Parameters.AddWithValue("@Contact", contact);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@dateOfOrder", DateTime.Now);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                int OrderDetailsID = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return OrderDetailsID;
            }
        }
        private OrderDetail FetchOrderDetails(int orderdetailsId)
        {
            // Example: Fetch order details from database
            // Replace with your actual database connection and query logic
            string connectionString = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString ;
            string query = "SELECT * FROM OrderDetails WHERE OrderDetailsID = @orderdetailsId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@orderdetailsId", orderdetailsId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Map data to OrderDetail object
                    OrderDetail orderDetail = new OrderDetail
                    {
                        OrderDetailID = Convert.ToInt32(reader["OrderDetailsID"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        // Add more properties as needed
                    };

                    return orderDetail;
                }
            }

            return null;
        }

        private User FetchUserDetails(int userID)
        {
            // Example: Fetch user details from database
            // Replace with your actual database connection and query logic
            string connectionString = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString; 
            string query = "SELECT * FROM Users WHERE ID = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", userID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Map data to User object
                    User user = new User
                    {
                        UserID = Convert.ToInt32(reader["ID"]),
                        Username = Convert.ToString(reader["Username"]),
                        Address = Convert.ToString(reader["Address"]),
                        // Add more properties as needed
                    };

                    return user;
                }
            }

            return null;
        }

        private List<Product> FetchProductsInCart(int userID)
        {
            // Example: Fetch products from cart including product details using a join
            List<Product> products = new List<Product>();

            string connectionString = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            string query = @"
        SELECT p.Name, p.Price, c.Quantity
        FROM Cart c
        INNER JOIN Product p ON c.ProductID = p.Id
        WHERE c.UserID = @UserID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Map data to Product object
                    Product product = new Product
                    {
                        ProductName = Convert.ToString(reader["Name"]),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        // Add more properties as needed
                    };

                    products.Add(product);
                }
            }

            return products;
        }

        private decimal CalculateTotalAmount(List<Product> products)
        {
            // Example: Calculate total amount based on products
            decimal totalAmount = 0;

            foreach (var product in products)
            {
                totalAmount += product.Price * product.Quantity;
            }

            return totalAmount;
        }
    }

    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int UserID { get; set; }
        // Add more properties as needed
    }

    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        // Add more properties as needed
    }

    public class Product
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        // Add more properties as needed
    }
}
