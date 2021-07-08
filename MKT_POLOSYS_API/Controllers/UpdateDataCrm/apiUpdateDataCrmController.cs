using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MKT_POLOSYS_API.Models;
using MKT_POLOSYS_API.Providers.UpdateDataCrm;

 
namespace MKT_POLOSYS_API.Controllers.UpdateDataCrm
{
    [Route("api/UpdateDataCrm")]
    [ApiController]

    public class apiGenerateCrmController : ControllerBase
    {
        // GET: api/<apiUploadStatusMssWiseController>
        
        [Produces("application/json")]

        [HttpPost("")]
        public async Task<IActionResult> postUpdateDataCrm()
        {
            
            string parameterBody = "";
            using (StreamReader stream = new StreamReader(Request.Body))
            {
                parameterBody = await stream.ReadToEndAsync();
            }
            apiUpdateDataCrmProvider procUpdDataCrm = new apiUpdateDataCrmProvider();
            var data = procUpdDataCrm.procUpdateCrmtoPolo(parameterBody);
            if (data.Count == 0)
            {
                return NotFound(data);
            }
            return Ok(data);
        }



    }
}
