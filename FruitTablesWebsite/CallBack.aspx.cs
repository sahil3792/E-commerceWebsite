using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FruitTablesWebsite
{
    public partial class CallBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProcessRazorpayResponse();
            }
        }

        private void ProcessRazorpayResponse()
        {
            try
            {
                // Retrieve parameters from Razorpay response
                string razorpay_payment_id = Request.Form["razorpay_payment_id"];
                string razorpay_order_id = Request.Form["razorpay_order_id"];
                string razorpay_signature = Request.Form["razorpay_signature"];

                Response.Redirect($"<script>alert({razorpay_payment_id})</script>");
                Response.Redirect($"<script>alert({razorpay_order_id})</script>");
                Response.Redirect($"<script>alert({razorpay_signature})</script>");

                // Validate the Razorpay signature (assuming you have a method to validate it)
                //bool isValidSignature = ValidateRazorpaySignature(razorpay_order_id, razorpay_payment_id, razorpay_signature);

                //if (isValidSignature)
                //{
                //    // Insert into orderdetails table
                //    InsertIntoOrderDetails(razorpay_payment_id, razorpay_order_id, razorpay_signature);

                //    // Display success message or redirect to a success page
                //    Response.Write("Payment successful! Thank you.");
                //}
                //else
                //{
                //    // Handle invalid signature
                //    Response.Write("Invalid Razorpay signature.");
                //}
            }
            catch (Exception ex)
            {
                // Log and handle exceptions
                Response.Write("Error processing payment: " + ex.Message);
            }
        }
        //private bool ValidateRazorpaySignature(string razorpay_order_id, string razorpay_payment_id, string razorpay_signature)
        //{
        //    // Implement your signature validation logic here
        //    // Example: Compare razorpay_signature with calculated signature based on your secret key and order/payment details
        //    // Return true if valid, false otherwise

        //    // Replace this with your actual signature validation logic
        //    string secret = "your_secret_key_here"; // Replace with your actual secret key
        //    string data = $"{razorpay_order_id}{razorpay_payment_id}";
        //    string calculatedSignature = CreateToken(data, secret); // Implement CreateToken method

        //    return calculatedSignature == razorpay_signature;
        //}

        private void InsertIntoOrderDetails(string payment_id, string order_id, string signature)
        {
            try
            {
                // Establish database connection
                using (SqlConnection conn = new SqlConnection("your_connection_string_here"))
                {
                    conn.Open();

                    // Insert into orderdetails table
                    string insertQuery = @"
                        INSERT INTO orderdetails (TotalAmount, UserID, DateOfOrder, OrderID, OrderRazorpayID, PaymentID, SignatureID)
                        VALUES (@TotalAmount, @UserID, @DateOfOrder, @OrderID, @OrderRazorpayID, @PaymentID, @SignatureID)";

                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@TotalAmount", decimal.Parse(Session["CheckoutAmount"].ToString()));
                    cmd.Parameters.AddWithValue("@UserID", int.Parse(Session["User"].ToString()));
                    cmd.Parameters.AddWithValue("@DateOfOrder", DateTime.Now);
                    cmd.Parameters.AddWithValue("@OrderID", int.Parse(Session["OrderID"].ToString())); // Assuming you have stored OrderID in Session
                    cmd.Parameters.AddWithValue("@OrderRazorpayID", order_id);
                    cmd.Parameters.AddWithValue("@PaymentID", payment_id);
                    cmd.Parameters.AddWithValue("@SignatureID", signature);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Successful insertion
                        Console.WriteLine("Data inserted into orderdetails table.");
                    }
                    else
                    {
                        // Insertion failed
                        Console.WriteLine("Failed to insert data into orderdetails table.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log and handle exceptions
                Console.WriteLine("Error inserting data into orderdetails table: " + ex.Message);
            }
        }
    }
}
