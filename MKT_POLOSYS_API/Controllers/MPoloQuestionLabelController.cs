using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MKT_POLOSYS_API.Models;
using MKT_POLOSYS_API.Providers;

namespace MKT_POLOSYS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MPoloQuestionLabelController : ControllerBase
    {
        // GET: api/<MPoloQuestionLabelController>
        [HttpGet("Data/Update")]
        [Produces("application/json")]

        public async Task<IActionResult> GetMPoloQuestionLabel()
        {
            var processResult = new ProcessResultModel();
            string parameterBody = "";
            using (StreamReader stream = new StreamReader(Request.Body))
            {
                parameterBody = await stream.ReadToEndAsync();
            }
            MMktPoloQuestionLabelProvider mMktPoloQuestionLabelProvider = new MMktPoloQuestionLabelProvider();
            var data = mMktPoloQuestionLabelProvider.getQuestionLabel(parameterBody);
             
            if (data.Count==0 )
            {
                return NotFound(data);
            }
            return Ok(data);
        }



        // GET api/<MPoloQuestionLabelController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MPoloQuestionLabelController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MPoloQuestionLabelController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MPoloQuestionLabelController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
