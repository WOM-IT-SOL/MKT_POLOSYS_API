using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MKT_POLOSYS_API.Models;
using MKT_POLOSYS_API.Providers.apiUploadStatusWise;

 
namespace MKT_POLOSYS_API.Controllers.apiUploadStatusMssWise
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateDataController : ControllerBase
    {
        // GET: api/<apiUploadStatusMssWiseController>
        [HttpPost("")]
        [Produces("application/json")]

        public async Task<IActionResult> procUpdateDataWise()
        {
            
            string parameterBody = "";
            using (StreamReader stream = new StreamReader(Request.Body))
            {
                parameterBody = await stream.ReadToEndAsync();
            }
            apiUploadStatusMssWiseProvider apiUploadStatusWise = new apiUploadStatusMssWiseProvider();
            var data = apiUploadStatusWise.procUpdStatWIse(parameterBody);

            if (data.Count == 0)
            {
                return NotFound(data);
            }
            return Ok(data);
        }



    }
}
