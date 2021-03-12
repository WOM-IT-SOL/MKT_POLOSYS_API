using Microsoft.EntityFrameworkCore;
using MKT_POLOSYS_API.DataAccess;
using MKT_POLOSYS_API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MKT_POLOSYS_API.Providers
{
    public class MasterCustAcqProvider
    {
        private WISE_STAGINGContext context = new WISE_STAGINGContext();

        public List<MasterCustAcqViewModel> getAllMasterCUst()//(int acqId)
        {
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            List<MasterCustAcqViewModel> masterCustAcqs = new List<MasterCustAcqViewModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    //Declare COnnection
                    var querySstring = "sp_MKT_POLO_GETMASTERCUSTACQALL";
                    SqlCommand command = new SqlCommand(querySstring, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    //Define Query Parameter
                    //command.Parameters.AddWithValue("@acqId", acqId);

                    //open Connection
                    command.Connection.Open();

                    //PRoses Sp
                    SqlDataReader rd = command.ExecuteReader();
                    while (rd.Read())
                    {
                        MasterCustAcqViewModel data = new MasterCustAcqViewModel();
                        data.acqId = Convert.ToInt32(rd["acqId"]);
                        data.partnerId = rd["partnerId"].ToString();
                        data.orderId = rd["orderId"].ToString();
                        data.transactionId = rd["transactionId"].ToString();
                        data.producTypeDesc = rd["producTypeDesc"].ToString();

                        masterCustAcqs.Add(data);
                    }

                    //Connection Close
                    command.Connection.Close();

                }
                catch (Exception ex)
                {

                    throw;
                }
               

            }

            return masterCustAcqs;

        }

    }
}
