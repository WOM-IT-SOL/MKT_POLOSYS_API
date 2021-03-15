using Microsoft.EntityFrameworkCore;
using MKT_POLOSYS_API.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MKT_POLOSYS_API.Providers
{
    public class PoloDukcapilProvider
    {
        private static WISE_STAGINGContext context = new WISE_STAGINGContext();
        private HttpClient client = new HttpClient();

        public static async Task getDukcapilQueue(string sourceData, string queueUID)
        {
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            //List<ProcessResultModel> mMktPoloQuestionLabels = new List<ProcessResultModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Declare Cnnection                
                var querySstring = "spMKT_POLO_GETDUKCAPILQUEUE";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = CommandType.StoredProcedure;

                // Define Query Parameter
                command.Parameters.AddWithValue("@sourceData", sourceData);
                command.Parameters.AddWithValue("@queueUID", queueUID);

                // Open Connection
                command.Connection.Open();

                List<Dictionary<string, object>> queue = new List<Dictionary<string, object>>();
                // Proses Sp
                SqlDataReader rd = command.ExecuteReader();
                while(rd.Read())
                {
                    queue.Add(Enumerable.Range(0, rd.FieldCount).ToDictionary(rd.GetName, rd.GetValue));
                }

                queue.ForEach(consumeDukcapil);

                //Connection Close
                command.Connection.Close();
            }
        }

        private static async void consumeDukcapil(Dictionary<string, object> data)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://10.0.9.227/api/dukcapil/");

            string bodyJSON = JsonConvert.SerializeObject(data, Formatting.Indented);
            var content = new StringContent(bodyJSON, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("check_dukcapil", content);
                Console.WriteLine(response.StatusCode);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("[ERROR]" + e.Message);
            }

        }
    }
}
