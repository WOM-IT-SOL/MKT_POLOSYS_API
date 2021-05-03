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
    public class MMktPoloQuestionLabelProvider 
    {
        private WISE_STAGINGContext context = new WISE_STAGINGContext();
      
        public List<ProcessResultModel> getQuestionLabel(string parameterBody)
        {            
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            List<ProcessResultModel> mMktPoloQuestionLabels = new List<ProcessResultModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Declare COnnection                
                var querySstring = "spMKT_POLO_APIPROCESS";
                SqlCommand command = new SqlCommand(querySstring, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //Define Query Parameter
                command.Parameters.AddWithValue("@parameterBody", parameterBody);

                //open Connection
                command.Connection.Open();

                //PRoses Sp
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    ProcessResultModel data = new ProcessResultModel();
                    data.responseMessage = rd[0].ToString();
                    data.responseCode = rd[1].ToString();
                    data.errorMessage = rd[2].ToString();
                    mMktPoloQuestionLabels.Add(data);
                }

                //Connection Close
                command.Connection.Close();
                Console.WriteLine("test");
            }

            return mMktPoloQuestionLabels;

        }
    }
}
