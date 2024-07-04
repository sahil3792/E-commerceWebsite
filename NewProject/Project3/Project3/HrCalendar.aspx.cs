using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;

namespace Project3
{
    public partial class HrCalendar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        public static string GetEvents()
        {
            string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            string query = "SELECT EventID, Title, StartDate, EndDate, Description FROM Events";
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            StringBuilder eventsJson = new StringBuilder();
            eventsJson.Append("[");
            foreach (DataRow row in dt.Rows)
            {
                eventsJson.Append("{");
                eventsJson.AppendFormat("\"id\": \"{0}\", \"title\": \"{1}\", \"start\": \"{2}\", \"end\": \"{3}\"",
                    row["EventID"], row["Title"], Convert.ToDateTime(row["StartDate"]).ToString("yyyy-MM-ddTHH:mm:ss"), Convert.ToDateTime(row["EndDate"]).ToString("yyyy-MM-ddTHH:mm:ss"));
                eventsJson.Append("},");
            }
            if (eventsJson.Length > 1)
            {
                eventsJson.Length--; // Remove the trailing comma
            }
            eventsJson.Append("]");

            return eventsJson.ToString();
        }

        [WebMethod]
        public static void AddEvent(string title, string startDate, string endDate, string description)
        {
            string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            string query = "INSERT INTO Events (Title, StartDate, EndDate, Description) VALUES (@Title, @StartDate, @EndDate, @Description)";
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(startDate));
                    cmd.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(endDate));
                    cmd.Parameters.AddWithValue("@Description", description);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        [WebMethod]
        public static void RemoveEvent(int eventID)
        {
            string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            string query = "DELETE FROM Events WHERE EventID = @EventID";
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@EventID", eventID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}