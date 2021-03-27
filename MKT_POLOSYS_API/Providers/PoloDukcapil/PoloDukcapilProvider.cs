using Microsoft.EntityFrameworkCore;
using MKT_POLOSYS_API.DataAccess;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MKT_POLOSYS_API.Providers.PoloDukcapil
{
    public class PoloDukcapilProvider
    {
        private static WISE_STAGINGContext context = new WISE_STAGINGContext();

        public static async Task getDukcapilQueue(string sourceData, string queueUID)
        {
            var connectionString = context.Database.GetDbConnection().ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                #region prepare connection for fetching dukcapil queue
                // Declare Connection                
                string sproc = "spMKT_POLO_DUKCAPIL_GETQUEUE";
                SqlCommand command = new SqlCommand(sproc, connection);
                command.CommandType = CommandType.StoredProcedure;

                // Define Query Parameter
                command.Parameters.AddWithValue("@sourceData", sourceData);
                command.Parameters.AddWithValue("@queueUID", queueUID);

                // Open Connection
                command.Connection.Open();
                #endregion

                #region fetch record and store to a list of dictionary
                List<Dictionary<string, object>> queue = new List<Dictionary<string, object>>();
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    queue.Add(Enumerable.Range(0, rd.FieldCount).ToDictionary(rd.GetName, rd.GetValue));
                }

                rd.Close();
                command.Connection.Close();
                #endregion

                #region fetch womf dukcapil api url
                string dukcapilUrl = "";

                string query = "SELECT PARAMETER_VALUE FROM M_MKT_POLO_PARAMETER WHERE PARAMETER_ID = 'URL_MKT_POLO_API_DUKCAPIL'";
                command = new SqlCommand(query, connection);
                command.CommandType = CommandType.Text;

                command.Connection.Open();

                rd = command.ExecuteReader();
                rd.Read();

                dukcapilUrl = rd.GetString(0);

                rd.Close();
                command.Connection.Close();
                #endregion

                #region consume womf dukcapil API for each record
                foreach (Dictionary<string, object> q in queue)
                {
                    DateTime reqDtm = DateTime.Now;
                    //consuming womf dukcapil API
                    var result = await consumeDukcapil(q, dukcapilUrl);

                    DateTime resDtm = DateTime.Now;

                    //prepare connection for update dukcapil result
                    sproc = "spMKT_POLO_DUKCAPIL_UPDATERESULT";
                    command = new SqlCommand(sproc, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Connection.Open();

                    //define query parameter
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@T_MKT_POLO_DUKCAPIL_CHECK_QUEUE_ID", q["T_MKT_POLO_DUKCAPIL_CHECK_QUEUE_ID"]);
                    command.Parameters.AddWithValue("@responseCode", result["ResponseCode"]);
                    command.Parameters.AddWithValue("@responseMessage", result["ResponseMessage"]);
                    command.Parameters.AddWithValue("@statusDukcapil", result["FinalResult"]);
                    command.Parameters.AddWithValue("@reqDtm", reqDtm.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    command.Parameters.AddWithValue("@resDtm", resDtm.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                    rd = command.ExecuteReader();

                    //close connection
                    command.Connection.Close();
                    rd.Close();
                }
                #endregion

                //Connection Close
                command.Connection.Close();
            }
        }

        private static async Task<Dictionary<string, string>> consumeDukcapil(Dictionary<string, object> data, string dukcapilUrl)
        {
            #region prepare request body
            Dictionary<string, object> body = new Dictionary<string, object>();
            body.Add("NIK", data["NIK"]);
            body.Add("NAMA_LGKP", data["NAMA_LGKP"]);
            body.Add("TGL_LHR", data["TGL_LHR"]);
            body.Add("TMPT_LHR", data["TMPT_LHR"]);
            body.Add("USER_NAME", "MarketingPool");
            body.Add("EMP_NAME", "MarketingPool");
            body.Add("OFFICE_CODE", "0011");
            body.Add("OFFICE_NAME", "Kantor Pusat");
            body.Add("REGION", "7");
            body.Add("CUST_NO", data["CUST_NO"]);
            //body.Add("APP_NO", "");
            body.Add("IP_USER", "10.0.9.66");
            body.Add("SOURCE", "MarketingPool");
            #endregion

            #region consume womf dukcapil API and return response
            HttpClient client = new HttpClient();

            string bodyJSON = JsonConvert.SerializeObject(body, Formatting.Indented);
            var content = new StringContent(bodyJSON, Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(new Uri(dukcapilUrl), content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(apiResponse);
                return result;
            }
            #endregion
        }
    }
}
