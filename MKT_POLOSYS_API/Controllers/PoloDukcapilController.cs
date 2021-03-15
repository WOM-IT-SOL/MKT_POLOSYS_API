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
        [HttpPost("api/CheckDukcapil")]
        [Produces("application/json")]

        public async Task<IActionResult> CheckDukcapil([FromBody]IDictionary<string, string> body)
        {
            string parameterBody = "";
            //using (StreamReader stream = new StreamReader(Request.Body))
            //{
            //    parameterBody = await stream.ReadToEndAsync();
            //}

            //Dictionary<string, string> test = new Dictionary<string, string>();

            //test.Add("test", "hi");
            await PoloDukcapilProvider.getDukcapilQueue(body["dataSource"], body["queueUID"]);

            return Ok(body);

            //MMktPoloQuestionLabelProvider mMktPoloQuestionLabelProvider = new MMktPoloQuestionLabelProvider();
            //var data = mMktPoloQuestionLabelProvider.getQuestionLabel(parameterBody);

            //if (data.Count == 0)
            //{
            //    return NotFound(data);
            //}
            //return Ok(data);
        }

        [HttpPost("api/test")]
        [Produces("application/json")]
        public async Task<IActionResult> test([FromBody]IDictionary<string, string> body)
        {
            return Ok("delete me please");
        }
    }
}