using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MKT_POLOSYS_API.Providers;

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
    }
}