using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MKT_POLOSYS_API.Providers.PoloDukcapil;
using Newtonsoft.Json;

namespace MKT_POLOSYS_API.Controllers.PoloDukcapil
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoloDukcapilController : ControllerBase
    {
        [HttpPost("")]
        [Produces("application/json")]

        public async Task<IActionResult> CheckDukcapil([FromBody]IDictionary<string, string> body)
        {
            await PoloDukcapilProvider.getDukcapilQueue(body["dataSource"], body["queueUID"], body.ContainsKey("isJob"));

            return Ok(new { });
        }
    }
}