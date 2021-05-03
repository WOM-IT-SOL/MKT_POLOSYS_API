using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MKT_POLOSYS_API.Models
{
    public class MasterCustAcqViewModel
    {
        public int acqId { get; set; } 
        public string partnerId { get; set; }
        public string orderId { get; set; }
        public string transactionId { get; set; }
        public string producTypeDesc { get; set; }

    }
}
