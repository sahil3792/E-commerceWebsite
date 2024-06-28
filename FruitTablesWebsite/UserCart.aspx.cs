using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Razorpay.Api;


namespace FruitTablesWebsite
{
    public partial class UserCart : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            conn = new SqlConnection(cs);
            conn.Open();

            if (!IsPostBack)
            {
                BindCart();
            }
        }
        private void BindCart()
        {
            DataTable dt = new DataTable();
            string query = $"exec DisplayCart {int.Parse(Session["User"].ToString())}";
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            rptCart.DataSource = dt;
            rptCart.DataBind();

            // Calculate subtotal and total
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

            // Update the UpdatePanel to reflect the changes
            UpdatePanel1.Update();
        }

        protected void rptCart_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int cartId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Minus")
            {
                // Handle decreasing quantity
                UpdateQuantity(cartId, -1);
            }
            else if (e.CommandName == "Plus")
            {
                // Handle increasing quantity
                UpdateQuantity(cartId, 1);
            }
            else if (e.CommandName == "Remove")
            {
                // Handle removing the product from the cart
                RemoveProductFromCart(cartId);
            }

            // Rebind the Repeater to reflect changes
            BindCart();
        }

        private void UpdateQuantity(int cartId, int change)
        {
            // Logic to update the product quantity in the database
            string connectionString = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateCartQuantity", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CartId", cartId);
                    cmd.Parameters.AddWithValue("@Change", change);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        private void RemoveProductFromCart(int cartId)
        {
            // Logic to remove the product from the cart in the database
            string connectionString = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("RemoveFromCart", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CartId", cartId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
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

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UserCheckOut.aspx");
        //    int UserID = int.Parse(Session["User"].ToString());
        //    decimal checkout_amount = decimal.Parse(lblTotal.Text);

        //    // Execute the SQL command to store order and retrieve orderId
        //    string query = $"exec StoreInOrders '{checkout_amount}', '{UserID}'; SELECT SCOPE_IDENTITY();";
        //    SqlCommand cmd = new SqlCommand(query, conn);
        //    int orderid = Convert.ToInt32(cmd.ExecuteScalar());

        //    // Optionally, store total checkout amount
        //    Session["CheckoutAmount"] = lblTotal.Text;

        //    // Retrieve customer details
        //    string selectuser = $"exec selectuser '{UserID}'";
        //    SqlCommand command = new SqlCommand(selectuser, conn);
        //    SqlDataReader rdr = command.ExecuteReader();
        //    string name = "", MobileNumber = "", email = "";

        //    if (rdr.Read())
        //    {
        //        name = rdr["Username"].ToString();
        //        MobileNumber = rdr["Phone_number"].ToString();
        //        email = rdr["Email"].ToString();
        //    }
        //    rdr.Close();

        //    // Retrieve Razorpay API keys from web.config
        //    string keyId = "rzp_test_J24VTErrWDMd0g";
        //    string keySecret = "VoRvJOlFc1hv9kNBS6xwWl2G";

        //    // Initialize Razorpay client
        //    RazorpayClient client = new RazorpayClient(keyId, keySecret);

        //    // Create Razorpay order
        //    Dictionary<string, object> options = new Dictionary<string, object>
        //{
        //    { "amount", (checkout_amount * 100) }, // Amount is in currency subunits. Hence, 50000 refers to 50000 paise or ₹500.
        //    { "currency", "INR" },
        //    { "receipt", orderid.ToString() },
        //    { "payment_capture", 1 }
        //};

        //    try
        //    {
        //        Order order = client.Order.Create(options);

        //        // Build HTML for Razorpay checkout
        //        StringBuilder outputHTML = new StringBuilder();
        //        outputHTML.Append("<html>");
        //        outputHTML.Append("<head>");
        //        outputHTML.Append("<title>Merchant Check Out Page</title>");
        //        outputHTML.Append("</head>");
        //        outputHTML.Append("<body>");
        //        outputHTML.Append("<center>Please do not refresh this page...</center>");
        //        outputHTML.Append("<form id='redirectForm' action='InvoiceGeneration.aspx' method='post'>");
        //        outputHTML.AppendFormat("<input type='hidden' name='razorpay_payment_id' value='{0}'/>", "");
        //        outputHTML.AppendFormat("<input type='hidden' name='razorpay_order_id' value='{0}'/>", order["id"]);
        //        outputHTML.AppendFormat("<input type='hidden' name='razorpay_signature' value='{0}'/>", "");
        //        outputHTML.AppendFormat("<input type='hidden' name='order_id' value='{0}'/>", orderid);
        //        outputHTML.AppendFormat("<input type='hidden' name='amount' value='{0}'/>", checkout_amount);
        //        outputHTML.AppendFormat("<input type='hidden' name='currency' value='INR'/>");
        //        outputHTML.AppendFormat("<input type='hidden' name='name' value='{0}'/>", name);
        //        outputHTML.AppendFormat("<input type='hidden' name='contact' value='{0}'/>", MobileNumber);
        //        outputHTML.AppendFormat("<input type='hidden' name='email' value='{0}'/>", email);
        //        outputHTML.Append("</form>");
        //        outputHTML.Append("<script src='https://checkout.razorpay.com/v1/checkout.js'></script>");
        //        outputHTML.Append("<script>");
        //        outputHTML.Append("var options = {");
        //        outputHTML.AppendFormat("key: '{0}',", keyId);
        //        outputHTML.AppendFormat("amount: '{0}',", (checkout_amount * 100).ToString());
        //        outputHTML.AppendFormat("currency: 'INR',");
        //        outputHTML.AppendFormat("name: 'Fruitables',");
        //        outputHTML.AppendFormat("description: 'Fruits',");
        //        outputHTML.AppendFormat("image: 'https://your_logo_url',");
        //        outputHTML.AppendFormat("order_id: '{0}',", order["id"]);
        //        outputHTML.Append("handler: function (response) {");
        //        outputHTML.Append("document.getElementById('redirectForm').razorpay_payment_id.value = response.razorpay_payment_id;");
        //        outputHTML.Append("document.getElementById('redirectForm').razorpay_signature.value = response.razorpay_signature;");
        //        outputHTML.Append("document.getElementById('redirectForm').submit();");
        //        outputHTML.Append("},");
        //        outputHTML.Append("prefill: {");
        //        outputHTML.AppendFormat("name: '{0}',", name);
        //        outputHTML.AppendFormat("email: '{0}',", email);
        //        outputHTML.AppendFormat("contact: '{0}'", MobileNumber);
        //        outputHTML.Append("},");
        //        outputHTML.Append("notes: {");
        //        outputHTML.Append("address: 'Note value'");
        //        outputHTML.Append("},");
        //        outputHTML.Append("theme: {");
        //        outputHTML.Append("color: '#3399cc'");
        //        outputHTML.Append("}");
        //        outputHTML.Append("};");
        //        outputHTML.Append("var rzp1 = new Razorpay(options);");
        //        outputHTML.Append("rzp1.open();");
        //        outputHTML.Append("</script>");
        //        outputHTML.Append("</body>");
        //        outputHTML.Append("</html>");

        //        Response.Write(outputHTML.ToString());
        //    }
        //    catch (Razorpay.Api.Errors.BadRequestError ex)
        //    {
        //        // Log and handle the BadRequestError specifically
        //        Response.Write($"BadRequestError: {ex.Message}");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log and handle other exceptions
        //        Response.Write($"Error: {ex.Message}");
        //    }
        }
    }


    }
