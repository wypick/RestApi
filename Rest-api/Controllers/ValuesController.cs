using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestApi;
using System.Text.Json;
using MySqlConnector;
using System.Data.SqlClient;
using System.Net;


namespace Rest_api.Controllers
{
    [Route("pass")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET http://host:port/pass/validate/{guid}
        [HttpGet("validate/{guid}")]
        public ActionResult<string> ValidateGet(string guid)
        {
            var time = DateTime.Now;
            
            var result = DbUtils.Get(guid);

            if (result == null)
            {
                return NotFound("404(NOT FOUND)");
            }

            else if ((result.DateFrom > time) || (result.DateTo < time))
            {
                return StatusCode(410);
            }
            else
            {
                return Ok("Валидно");
            }
        }

        // GET http://host:port/pass/{guid}
        [HttpGet("{guid}")]
        public ActionResult<string> Get(string guid)
        {
            var result = DbUtils.Get(guid);
            if (result == null)
            {
                return NotFound("404(NOT FOUND)");
            }
            return Ok(JsonSerializer.Serialize(result));
        }

        // POST http://host:port/pass/
        [HttpPost]
        public IActionResult Post(Pass pass)
        {
            //var result = JsonSerializer.Deserialize<Pass>(pass);
            return Ok(JsonSerializer.Serialize(DbUtils.Post(pass))); 
        }

        // PUT http://host:port/pass/
        [HttpPut]
        public IActionResult Put(Pass pass)
        {
            if (DbUtils.Put(pass))
            {
                return Ok();
            }
            else
            {
                return NotFound("404(NOT FOUND)");
            }
        }

        // DELETE http://host:port/pass/{guid}
        [HttpDelete("{guid}")]
        public IActionResult Delete(string guid)
        {
            if (DbUtils.Delete(guid))
            {
                return Ok();
            }
            else
            {
                return NotFound("404(NOT FOUND)");
            }
        }
    }
}
