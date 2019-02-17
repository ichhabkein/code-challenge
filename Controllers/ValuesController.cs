using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;

namespace search_api.Controllers
{
    [Route("api")]
    [ApiController]
    public class ValuesController : ControllerBase
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

        private ActionResult<string> handleQuery (string arguments)
        {
            // convert phrases to comma delimited terms as a hack
            // if more time would allow quoting of terms
            if (arguments != null) {
                arguments = arguments.Trim().Replace(' ',',');  
            } else {
                arguments = "";
            }

            FlickrIngest ingest = new FlickrIngest(true);

            // get data from flickr
            ingest.SearchFlickr(arguments);

            return JsonConvert.SerializeObject((LimitedFlickrFeed)ingest.GetFeed());

        }

    }
}
