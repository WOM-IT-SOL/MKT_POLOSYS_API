using Microsoft.EntityFrameworkCore;
using MKT_POLOSYS_API.DataAccess;
using MKT_POLOSYS_API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MKT_POLOSYS_API.Providers.get_statusprospek_polo
{
    public class getStatusProspekPoloProvider
    {
        private WISE_STAGINGContext context = new WISE_STAGINGContext();
        public  ProcessResultModelStatViewModel procGetStatusProspek(string taskId)
        {
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            ProcessResultModelStatViewModel getStatusProspeks = new ProcessResultModelStatViewModel();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = "spMKT_POLOCRM_GETSTATUSPROSPEK";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //Define Query Parameter
                command.Parameters.AddWithValue("@taskId", taskId);

                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    getStatusProspeks.prospectStat = rd[0].ToString();
                    getStatusProspeks.responseCode = rd[1].ToString();
                    getStatusProspeks.responseMessage = rd[2].ToString();
                    getStatusProspeks.errorMessage = rd[3].ToString();

                }

                //Connection Close
                command.Connection.Close();

            }

            return getStatusProspeks;

        }
    }
}
