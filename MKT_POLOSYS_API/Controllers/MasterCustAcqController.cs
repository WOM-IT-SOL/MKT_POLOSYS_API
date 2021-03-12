using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MKT_POLOSYS_API.Models;
using MKT_POLOSYS_API.Providers;


namespace MKT_POLOSYS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterCustAcqController : ControllerBase
    {
        // GET: api/<MasterCustAcqController>
        [HttpGet("Data/GetAll")]
        [Produces("application/json")]
        public ActionResult<List<MasterCustAcqViewModel>> Get()
        {
            MasterCustAcqProvider masterCustAcqProvider = new MasterCustAcqProvider();
            var data = masterCustAcqProvider.getAllMasterCUst();
            return data;
        }

        // GET api/<MasterCustAcqController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MasterCustAcqController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MasterCustAcqController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MasterCustAcqController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
