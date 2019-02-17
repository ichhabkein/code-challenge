using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;


// for diagnostics we only accept the one argument - 'q', for the search terms
// to be applied to the flickr tags.  we assume any spaces in the input should
// be treated as tag delimiters and replace them with commas.

namespace search_api.Controllers
{
    [Route("")]
    [ApiController]
   public class DiagnosticsController : ControllerBase
    {
        // GET
        [HttpGet("")]
        public ActionResult<string> Get(string q)
        {
            return handleQuery(q);
        }

        // POST
        [HttpPost("")]
        public ActionResult<string> Post([FromForm] string q)
        {
            return handleQuery(q);
        }

        private ActionResult<string>  handleQuery (string arguments)
        {
            // convert phrases to comma delimited terms as a hack
            // if more time would allow quoting of terms
            if (arguments != null) {
                arguments = arguments.Trim().Replace(' ',',');  
            } else {
                arguments = "";
            }

            Diagnostics page = new Diagnostics(arguments);

            // while our API allows empty arguments
            // (flickr will just pass back current results)
            // the diagnostics page explicitly diallows this
            // as a design choice.

            if(arguments == "") {
                return page.GenerateEmptyDiagnosticPage();
            } else {
                return page.GenerateDiagnosticPage();
            }

        }

    }

}