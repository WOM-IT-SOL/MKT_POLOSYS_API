using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MKT_POLOSYS_API.Models;
using MKT_POLOSYS_API.Providers.DataCampaignCrmToPolo;

 
namespace MKT_POLOSYS_API.Controllers.DataCampaignCrmToPolo
{
    [Route("api/apiGenerateCrm")]
    [ApiController]
    public class apiGenerateCrmController : ControllerBase
    {
        // GET: api/<apiUploadStatusMssWiseController>
        
        [Produces("application/json")]

        [HttpPost("procGenerateDataCrm")]
        public async Task<IActionResult> postGenerateDataCrm()
        {
            
            string parameterBody = "";
            using (StreamReader stream = new StreamReader(Request.Body))
            {
                parameterBody = await stream.ReadToEndAsync();
            }
            apiGenerateCrmProvider procGenDataCrm = new apiGenerateCrmProvider();
            var data = procGenDataCrm.procGenDataCrm(parameterBody);

            if (data.Count == 0)
            {
                return NotFound(data);
            }
            return Ok(data);
        }



    }
}
