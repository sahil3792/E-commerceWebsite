using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FruitTablesWebsite
{
    public partial class InvoiceGeneration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string paymentId = Request.Form["razorpay_payment_id"];
            string orderId = Request.Form["razorpay_order_id"];
            string signature = Request.Form["razorpay_signature"];
            int orderID = int.Parse(Request.Form["order_id"]);
            decimal amount = decimal.Parse(Request.Form["amount"]);
            string currency = Request.Form["currency"];
            string name = Request.Form["name"];
            string contact = Request.Form["contact"];
            string email = Request.Form["email"];

            if (VerifyPaymentSignature(orderId, paymentId, signature))
            {
                // Payment is successful and verified
                // Proceed with invoice generation
                GenerateInvoice(orderID, paymentId, amount, name, contact, email);
            }
            else
            {
                // Handle invalid signature scenario
                Response.Write("Invalid payment signature");
            }
        }

        private bool VerifyPaymentSignature(string orderId, string paymentId, string razorpaySignature)
        {
            string secret = "VoRvJOlFc1hv9kNBS6xwWl2G";
            string payload = $"{orderId}|{paymentId}";

            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
                var generatedSignature = BitConverter.ToString(hash).Replace("-", "").ToLower();

                return generatedSignature == razorpaySignature;
            }
        }

        private void GenerateInvoice(int orderID, string paymentId, decimal amount, string name, string contact, string email)
        {
            // Implement your invoice generation logic here
            // You can use the paymentId, orderID, amount, name, contact, and email for reference

            Response.Write($"Invoice generated successfully for Order ID: {orderID}, Payment ID: {paymentId}, Amount: {amount}, Name: {name}, Contact: {contact}, Email: {email}");
        }
    }
}