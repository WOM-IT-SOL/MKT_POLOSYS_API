﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKT_POLOSYS_API.Models
{
    public class ProcessResultModel
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        
        public string errorMessage { get; set; }
    }
}
