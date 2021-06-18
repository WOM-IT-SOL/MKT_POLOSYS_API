using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKT_POLOSYS_API.Models
{
    public class getStatusProspekPoloViewModel
    {
        public string taskId { get; set; }
    }

    public class ProcessResultModelStatViewModel
    {
        public string prospectStat { get; set; }
        public string responseCode { get; set; }
        public string responseMessage { get; set; }

    }
}
