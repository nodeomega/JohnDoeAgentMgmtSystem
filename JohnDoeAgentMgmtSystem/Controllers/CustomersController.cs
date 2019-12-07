using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using JohnDoeAgentMgmtSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JohnDoeAgentMgmtSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // GET: api/Customers
        [HttpGet]
        public IActionResult Get()
        {
            // Set JSON serializer options.
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            try
            {
                // get the JSON file text.
                var customerJsonFileString = System.IO.File.ReadAllText(@"Data\customers.json");

                // Deserialize the JSON into a list of Agents.
                var customerJsonModel = JsonSerializer.Deserialize<List<Customer>>(customerJsonFileString, options);

                // Return the list of Agents.
                return Ok(customerJsonModel);
            }
            catch (Exception e)
            {
                // return an error message with a 500 Server Error HTTP Code.
                return StatusCode(500, e.Message);
            }
        }

        // GET: api/Customers/5
        [HttpGet("{id}", Name = "Get[controller]")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Customers
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
