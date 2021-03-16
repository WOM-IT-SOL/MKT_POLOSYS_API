using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MKT_POLOSYS_API.Providers;
using Newtonsoft.Json;

namespace MKT_POLOSYS_API.Controllers
{
    public class PoloDukcapilController : ControllerBase
    {
        [HttpPost("api/[controller]")]
        [Produces("application/json")]

        public async Task<IActionResult> CheckDukcapil([FromBody]IDictionary<string, string> body)
        {
            await PoloDukcapilProvider.getDukcapilQueue(body["dataSource"], body["queueUID"]);

            return Ok();
        }
    }
}