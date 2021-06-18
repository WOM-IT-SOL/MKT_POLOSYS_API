using Microsoft.AspNetCore.Mvc;
using MKT_POLOSYS_API.Models;
using MKT_POLOSYS_API.Providers.get_statusprospek_polo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MKT_POLOSYS_API.Controllers.get_statusprospek_polo
{
    [Route("api/[controller]")]
    [ApiController]
    public class getStatusProspectController : ControllerBase
    {
        [HttpGet("{taskId}")]
        [Produces("application/json")]
        public ActionResult<ProcessResultModelStatViewModel> GetOne(string taskId)
        {
            getStatusProspekPoloProvider getStatusProspekPoloProvider = new getStatusProspekPoloProvider();
            var data = getStatusProspekPoloProvider.procGetStatusProspek(taskId);
            return data;
        }
    }
}
