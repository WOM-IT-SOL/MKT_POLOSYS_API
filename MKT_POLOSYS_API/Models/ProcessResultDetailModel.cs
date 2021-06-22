using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKT_POLOSYS_API.Models
{
    public class ProcessResultDetailModel
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<ErrorDetailModel> errorMessage { get; set; }
    }
}
