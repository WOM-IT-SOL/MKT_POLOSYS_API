﻿using Microsoft.AspNetCore.Mvc;
using MKT_POLOSYS_API.Providers.data_voidsla_crm_to_polo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MKT_POLOSYS_API.Controllers.data_voidsla_crm_to_polo
{
    [Route("api/[controller]")]
    [ApiController]
    public class dataVoidSLaCrmToPoloController : ControllerBase
    {
        [Produces("application/json")]

        [HttpPost("")]
        public async Task<IActionResult> postDataVoidSlaCrmToPolo()
        {
            string guid = System.Guid.NewGuid().ToString().ToUpper();
            string parameterBody = "";
            using (StreamReader stream = new StreamReader(Request.Body))
            {
                parameterBody = await stream.ReadToEndAsync();
            }
            dataVoidSLaCrmToPoloProvider dataVoidSLaCrmToPolo = new dataVoidSLaCrmToPoloProvider();
            var done = "not done";
            var data = dataVoidSLaCrmToPolo.procDataVoidSla(guid, out done, parameterBody);
            //var task= Task.Run(() => dataVoidSLaCrmToPolo.UpdateVoidFlag(guid));
            //task.Wait();

            if (data.Count == 0)
            {
                return NotFound(data);
            }
            return Ok(data);
        }
    }
}
