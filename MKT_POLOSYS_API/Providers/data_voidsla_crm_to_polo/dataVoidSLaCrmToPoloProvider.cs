using Microsoft.EntityFrameworkCore;
using MKT_POLOSYS_API.DataAccess;
using MKT_POLOSYS_API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MKT_POLOSYS_API.Providers.data_voidsla_crm_to_polo
{
    public class dataVoidSLaCrmToPoloProvider
    {
        private WISE_STAGINGContext context = new WISE_STAGINGContext();

        
        public List<ProcessResultDetailModel> procDataVoidSla(string guid, out string done,string parameterBody)
        {
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            List<ProcessResultDetailModel> procGenDataCrm = new List<ProcessResultDetailModel>();
            List<ErrorDetailModel> ListDetailError = new List<ErrorDetailModel>(); 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                try
                {
                    //Declare COnnection                
                    var querySstring = "SPMKT_POLOCRM_UPDATE_VOIDSLA_VAL";
                    SqlCommand command = new SqlCommand(querySstring, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    //Define Query Parameter
                    command.Parameters.AddWithValue("@parameterBody", parameterBody);
                    command.Parameters.AddWithValue("@guid", guid);

                    //open Connection
                    command.Connection.Open();
                    command.CommandTimeout=7200; //set timeout 2Jam (3600x2)
                    //PRoses Sp
                    Console.WriteLine("Execute StoreProcedure Validation Started..");
                    SqlDataReader rd = command.ExecuteReader();
                    ProcessResultDetailModel data = new ProcessResultDetailModel();
                    while (rd.Read())
                    {
                        data.responseCode = rd[0].ToString();
                        data.responseMessage = rd[1].ToString();
                        //data.errorMessage = rd[2].ToString();
                    }
                    if (rd.NextResult())
                    {
                        while (rd.Read())
                        { 
                            ErrorDetailModel listError = new ErrorDetailModel();
                            listError.taskId = rd[0].ToString();
                            listError.errDesc = rd[1].ToString();
                            ListDetailError.Add(listError); 
                        }
                        data.errorMessage = ListDetailError.ToList();

                    }
                    procGenDataCrm.Add(data);
                    //Connection Close
                    command.Connection.Close();
                    Console.WriteLine("Execute StoreProcedure Validation Finished..");
                    done = "done";
                }
                catch (Exception e)
                {
                    Console.WriteLine("Execute StoreProcedure Validation Error..");
                    ProcessResultDetailModel data = new ProcessResultDetailModel();
                    data.responseCode = "02";
                    data.responseMessage = e.Message;
                    done = "not done";
                    procGenDataCrm.Add(data); 
                }
                
            }
            
            if (done == "done")
            {
                Task.Run(() => UpdateVoidFlag( guid));
            }
            return procGenDataCrm;

        }

        public async Task UpdateVoidFlag(string guid)
        {
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            List<ProcessResultDetailModel> procGenDataCrm = new List<ProcessResultDetailModel>();
            List<ErrorDetailModel> ListDetailError = new List<ErrorDetailModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = "spMKT_POLOCRM_MAINGATE";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //Define Query Parameter
                command.Parameters.AddWithValue("@guid", guid);
                //command.Parameters.AddWithValue("", "23");

                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                 
                //Connection Close
                command.Connection.Close();

            }
        }


    }
}
