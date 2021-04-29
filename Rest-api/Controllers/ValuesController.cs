using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestApi;
using System.Text.Json;

namespace Rest_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }
        // GET api/values/5
        [HttpGet("{guid}")]
        public ActionResult<string> Get(string guid)
        {
            var result = DbUtils.Get(guid);
            return JsonSerializer.Serialize(result);
        }

        // POST api/values
        [HttpPost]
        public string Post(string pass)
        {
            var result = JsonSerializer.Deserialize<Pass>(pass);
            return DbUtils.Post(result);
        }

        // PUT api/values/5 http://host:port/pass/
        [HttpPut("{pass}")]
        public void Put(string pass)
        {
            var result = JsonSerializer.Deserialize<Pass>(pass);
            DbUtils.Put(result);
        }

        // DELETE api/values/5
        [HttpDelete("{guid}")]
        public void Delete(string guid)
        {
            DbUtils.Delete(guid);
        }
    }
}
