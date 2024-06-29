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
using System.Drawing.Drawing2D;
using Razorpay.Api;
using System.Data.SqlTypes;
using System.Text;
using System.Xml.Linq;



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
                BindUserDetails();
                
                BindCheckoutCart();
                CalculateTotal();

            }
        }

        private void BindUserDetails()
        {
            string userId = Session["User"].ToString();
            DataTable dt = new DataTable();
            bool hasNullFields = false;

            string query = $"SELECT FName, LName,  Address, TownCity, Country, PostalCode, Phone_number FROM Users WHERE ID = '{userId}'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                TextBox1.Text = row["FName"]?.ToString();
                TextBox2.Text = row["LName"]?.ToString();
                TextBox4.Text = row["Address"]?.ToString();
                TextBox5.Text = row["TownCity"]?.ToString();
                TextBox6.Text = row["Country"]?.ToString();
                TextBox7.Text = row["PostalCode"]?.ToString();
                TextBox8.Text = row["Phone_number"]?.ToString();

                if (string.IsNullOrEmpty(TextBox1.Text) || string.IsNullOrEmpty(TextBox2.Text) || 
            string.IsNullOrEmpty(TextBox4.Text) ||
            string.IsNullOrEmpty(TextBox5.Text) ||
            string.IsNullOrEmpty(TextBox6.Text) ||
            string.IsNullOrEmpty(TextBox7.Text) ||
            string.IsNullOrEmpty(TextBox8.Text))
                {
                    hasNullFields = true;
                }
            }
            else
            {
                hasNullFields = true;
            }

            if (hasNullFields)
            {
                // Notify the user to fill in the textboxes
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Fill in the billing details');", true);
                btnPlaceOrder.Visible = false;
            }
            else
            {
                
            }
        }

        private string CreateToken(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);

            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

        protected void PaymentGateway()
        {

            int UserID = int.Parse(Session["User"].ToString());
            decimal totalAmount = Convert.ToDecimal(Session["TotalAmount"]);

            // Execute the SQL command to store order and retrieve orderId
            string query = $"exec StoreInOrders '{totalAmount}', '{UserID}'; SELECT SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(query, conn);
            int orderid = Convert.ToInt32(cmd.ExecuteScalar());

            // Optionally, store total checkout amount
            Session["CheckoutAmount"] = lblTotal.Text;

            // Retrieve customer details
            string selectuser = $"exec selectuser '{UserID}'";
            SqlCommand command = new SqlCommand(selectuser, conn);
            SqlDataReader rdr = command.ExecuteReader();
            string name = "", MobileNumber = "", email = "";

            if (rdr.Read())
            {
                name = rdr["Username"].ToString();
                MobileNumber = rdr["Phone_number"].ToString();
                email = rdr["Email"].ToString();
            }
            rdr.Close();

            // Retrieve Razorpay API keys from web.config
            string keyId = "rzp_test_J24VTErrWDMd0g";
            string keySecret = "VoRvJOlFc1hv9kNBS6xwWl2G";

            // Initialize Razorpay client
            RazorpayClient client = new RazorpayClient(keyId, keySecret);

            // Create Razorpay order
            Dictionary<string, object> options = new Dictionary<string, object>
            {
                { "amount", (totalAmount * 100) }, // Amount is in currency subunits. Hence, 50000 refers to 50000 paise or ₹500.
                { "currency", "INR" },
                { "receipt", orderid.ToString() },
                { "payment_capture", 1 }
            };

            try
            {
                Order order = client.Order.Create(options);

                // Build HTML for Razorpay checkout
                StringBuilder outputHTML = new StringBuilder();
                outputHTML.Append("<html>");
                outputHTML.Append("<head>");
                outputHTML.Append("<title>Merchant Check Out Page</title>");
                outputHTML.Append("</head>");
                outputHTML.Append("<body>");
                outputHTML.Append("<center>Please do not refresh this page...</center>");
                outputHTML.Append("<form id='redirectForm' action='InvoiceGeneration.aspx' method='post'>");
                outputHTML.AppendFormat("<input type='hidden' name='razorpay_payment_id' value='{0}'/>", "");
                outputHTML.AppendFormat("<input type='hidden' name='razorpay_order_id' value='{0}'/>", order["id"]);
                outputHTML.AppendFormat("<input type='hidden' name='razorpay_signature' value='{0}'/>", "");
                outputHTML.AppendFormat("<input type='hidden' name='order_id' value='{0}'/>", orderid);
                outputHTML.AppendFormat("<input type='hidden' name='amount' value='{0}'/>", totalAmount);
                outputHTML.AppendFormat("<input type='hidden' name='currency' value='INR'/>");
                outputHTML.AppendFormat("<input type='hidden' name='name' value='{0}'/>", name);
                outputHTML.AppendFormat("<input type='hidden' name='contact' value='{0}'/>", MobileNumber);
                outputHTML.AppendFormat("<input type='hidden' name='email' value='{0}'/>", email);
                outputHTML.Append("</form>");
                outputHTML.Append("<script src='https://checkout.razorpay.com/v1/checkout.js'></script>");
                outputHTML.Append("<script>");
                outputHTML.Append("var options = {");
                outputHTML.AppendFormat("key: '{0}',", keyId);
                outputHTML.AppendFormat("amount: '{0}',", (totalAmount * 100).ToString());
                outputHTML.AppendFormat("currency: 'INR',");
                outputHTML.AppendFormat("name: 'Fruitables',");
                outputHTML.AppendFormat("description: 'Fruits',");
                outputHTML.AppendFormat("image: 'https://your_logo_url',");
                outputHTML.AppendFormat("order_id: '{0}',", order["id"]);
                outputHTML.Append("handler: function (response) {");
                outputHTML.Append("document.getElementById('redirectForm').razorpay_payment_id.value = response.razorpay_payment_id;");
                outputHTML.Append("document.getElementById('redirectForm').razorpay_signature.value = response.razorpay_signature;");
                outputHTML.Append("document.getElementById('redirectForm').submit();");
                outputHTML.Append("},");
                outputHTML.Append("prefill: {");
                outputHTML.AppendFormat("name: '{0}',", name);
                outputHTML.AppendFormat("email: '{0}',", email);
                outputHTML.AppendFormat("contact: '{0}'", MobileNumber);
                outputHTML.Append("},");
                outputHTML.Append("notes: {");
                outputHTML.Append("address: 'Note value'");
                outputHTML.Append("},");
                outputHTML.Append("theme: {");
                outputHTML.Append("color: '#3399cc'");
                outputHTML.Append("}");
                outputHTML.Append("};");
                outputHTML.Append("var rzp1 = new Razorpay(options);");
                outputHTML.Append("rzp1.open();");
                outputHTML.Append("</script>");
                outputHTML.Append("</body>");
                outputHTML.Append("</html>");

                Response.Write(outputHTML.ToString());
            }
            catch (Razorpay.Api.Errors.BadRequestError ex)
            {
                // Log and handle the BadRequestError specifically
                Response.Write($"BadRequestError: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log and handle other exceptions
                Response.Write($"Error: {ex.Message}");
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

            
            

            decimal subtotal = 0;
            foreach (DataRow row in dt.Rows)
            {
                subtotal += Convert.ToDecimal(row["Price"]) * Convert.ToInt32(row["Quantity"]);
            }

            decimal shipping = 30; // Flat rate shipping
            decimal total = subtotal + shipping;

            Session["TotalAmount"] = total;

            // Bind subtotal and total to labels
            lblSubtotal.Text = subtotal.ToString("F2");
            lblTotal.Text = total.ToString("F2");
        }

        protected void btnSaveBillingDetails_Click(object sender, EventArgs e)
        {
            string userId = Session["User"].ToString();
            string firstName = TextBox1.Text.Trim();
            string lastName = TextBox2.Text.Trim();
            string address = TextBox4.Text.Trim();
            string city = TextBox5.Text.Trim();
            string country = TextBox6.Text.Trim();
            string postalCode = TextBox7.Text.Trim();
            string mobile = TextBox8.Text.Trim();
            string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "UPDATE Users SET FName = @FName, LName = @LName, Address = @Address, TownCity = @City, Country = @Country, PostalCode = @PostalCode, Phone_number = @Mobile WHERE ID = @UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FName", firstName);
                cmd.Parameters.AddWithValue("@LName", lastName);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@City", city);
                cmd.Parameters.AddWithValue("@Country", country);
                cmd.Parameters.AddWithValue("@PostalCode", postalCode);
                cmd.Parameters.AddWithValue("@Mobile", mobile);
                cmd.Parameters.AddWithValue("@UserId", userId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

                if (rowsAffected > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Billing details saved successfully.');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Failed to save billing details.');", true);
                }
            }
        }
        private void CalculateTotal()
        {
            if (rptCheckoutCart.DataSource != null)
            {
                DataTable dt = (DataTable)rptCheckoutCart.DataSource;
                decimal subtotal = 0;

                foreach (DataRow row in dt.Rows)
                {
                    decimal price = Convert.ToDecimal(row["Price"]);
                    int quantity = Convert.ToInt32(row["Quantity"]);
                    subtotal += price * quantity;
                }

                decimal shipping = 30; // Flat rate shipping
                decimal total = subtotal + shipping;

                Session["TotalAmount"] = total;

                // Bind subtotal and total to labels
                lblSubtotal.Text = subtotal.ToString("F2");
                lblTotal.Text = total.ToString("F2");
            }
            else
            {
                // Handle case where DataSource is null (or not assigned correctly)
                // You can log an error, display a message, or take appropriate action
                Response.Write("DataSource is null or not assigned correctly.");
            }
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            PaymentGateway();
        }
    }
}