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
        public IActionResult Get(int id)
        {
            // Return a 500 error code as getting a single customer wasn't in the exercise parameters.
            // Return a StatusCode object rather than a NotImplementedException for security reasons (do not provide stack trace).
            return StatusCode(500, "Single Customer GET will not be implemented for this exercise.");

        }

        // POST: api/Customers
        [HttpPost]
        public IActionResult Post([FromBody] Customer value)
        {
            // Set JSON serializer options.
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var agentsFileName = @"Data\agents.json";
            var customersFileName = @"Data\customers.json";

            try
            {
                // get the Agents JSON file text.
                var agentJsonFileString = System.IO.File.ReadAllText(agentsFileName);

                // Deserialize the Agents JSON into a list of Agents.
                var agentJsonModel = JsonSerializer.Deserialize<List<Agent>>(agentJsonFileString, options);

                // get the values for the new customer.
                var newCustomer = value;

                // Check if a valid Agent ID was specified, and fail if the agent ID does not exist.
                if (!agentJsonModel.Exists(agent => agent.Id == newCustomer.AgentId))
                {
                    return BadRequest(
                        $"Unable to add Customer:  There is no agent ID matching the specified Agent ID: {value.AgentId}.");
                }

                // Create a new GUID for the new customer.
                newCustomer.Guid = Guid.NewGuid();

                // If the AgentID check passes, get the Customers JSON file text.
                var customerJsonFileString = System.IO.File.ReadAllText(customersFileName);

                // Deserialize the Customers JSON into a list of Agents.
                var customerJsonModel = JsonSerializer.Deserialize<List<Customer>>(customerJsonFileString, options);
                
                var isDuplicateId = customerJsonModel.Exists(customer => customer.Id == newCustomer.Id);

                if (isDuplicateId)
                {
                    // If there is already an agent with the specified ID, return a 400 HTTP code and an error message.
                    return BadRequest($"There is already a customer with ID {value.Id}");
                }

                // Add the agent to the list of customers.
                customerJsonModel.Add(newCustomer);

                // serialize the updated list of Customers.
                var serializedCustomers = JsonSerializer.Serialize(customerJsonModel, options);

                // Rewrite the updated file.
                System.IO.File.WriteAllText(customersFileName, serializedCustomers);

                // Return the inserted customer.
                return Ok(newCustomer);
            }
            catch (Exception e)
            {
                // return an error message with a 500 Server Error HTTP Code.
                return StatusCode(500, e.Message);
            }
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer value)
        {
            // Set JSON serializer options.
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var agentsFileName = @"Data\agents.json";
            var customersFileName = @"Data\customers.json";

            try
            {
                // get the Agents JSON file text.
                var agentJsonFileString = System.IO.File.ReadAllText(agentsFileName);

                // Deserialize the Agents JSON into a list of Agents.
                var agentJsonModel = JsonSerializer.Deserialize<List<Agent>>(agentJsonFileString, options);

                // get the values for the customer being updated.
                var updateCustomer = value;

                // Check if a valid Agent ID was specified, and fail if the agent ID does not exist.
                if (!agentJsonModel.Exists(agent => agent.Id == updateCustomer.AgentId))
                {
                    return BadRequest(
                        $"Unable to update Customer:  There is no agent ID matching the specified Agent ID: {value.AgentId}.");
                }

                // If the AgentID check passes, get the Customers JSON file text.
                var customerJsonFileString = System.IO.File.ReadAllText(customersFileName);

                // Deserialize the Customers JSON into a list of Agents.
                var customerJsonModel = JsonSerializer.Deserialize<List<Customer>>(customerJsonFileString, options);

                var isFound = customerJsonModel.Exists(customer => customer.Id == id);

                if (!isFound)
                {
                    // If the agent with the specified ID doesn't exist, return a 400 HTTP code and an error message.
                    return BadRequest($"Unable to update: There is no customer with ID {value.Id}");
                }

                // use a foreach loop to find the specified agent by ID, update the data, and then exit the loop
                // (rather than use LINQ).
                foreach (var t in customerJsonModel)
                {
                    // If it's not the specified agent, go on to the next.
                    if (t.Id != updateCustomer.Id) continue;
                    
                    t.AgentId = updateCustomer.AgentId;
                    t.Guid = updateCustomer.Guid;
                    t.IsActive = updateCustomer.IsActive;
                    t.Balance = updateCustomer.Balance;
                    t.Age = updateCustomer.Age;
                    t.EyeColor = updateCustomer.EyeColor;
                    t.Name = updateCustomer.Name;
                    t.Company = updateCustomer.Company;
                    t.Email = updateCustomer.Email;
                    t.Phone = updateCustomer.Phone;
                    t.Address = updateCustomer.Address;
                    t.Registered = updateCustomer.Registered;
                    t.Latitude = updateCustomer.Latitude;
                    t.Longitude = updateCustomer.Longitude;
                    t.Tags = updateCustomer.Tags;
                    break;
                }


                // serialize the updated list of Customers.
                var serializedCustomers = JsonSerializer.Serialize(customerJsonModel, options);

                // Rewrite the updated file.
                System.IO.File.WriteAllText(customersFileName, serializedCustomers);

                // Return the inserted customer.
                return Ok(updateCustomer);
            }
            catch (Exception e)
            {
                // return an error message with a 500 Server Error HTTP Code.
                return StatusCode(500, e.Message);
            }
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Set JSON serializer options.
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var customersFileName = @"Data\customers.json";

            try
            {
                // If the AgentID check passes, get the Customers JSON file text.
                var customerJsonFileString = System.IO.File.ReadAllText(customersFileName);

                // Deserialize the Customers JSON into a list of Agents.
                var customerJsonModel = JsonSerializer.Deserialize<List<Customer>>(customerJsonFileString, options);

                var isFound = customerJsonModel.Exists(customer => customer.Id == id);

                if (!isFound)
                {
                    // If the agent with the specified ID doesn't exist, return a 400 HTTP code and an error message.
                    return BadRequest($"Unable to delete: There is no customer with ID {id}");
                }

                // Get the customer to be deleted.
                var deleteCustomer = customerJsonModel.FirstOrDefault(customer => customer.Id == id);

                // Delete the customer, and store the result.
                var deleted = customerJsonModel.Remove(deleteCustomer);

                if (!deleted)
                {
                    // If the agent with the specified ID doesn't exist, return a 400 HTTP code and an error message.
                    return BadRequest($"Deletion of customer with ID {id} failed.");
                }

                // serialize the updated list of Customers.
                var serializedCustomers = JsonSerializer.Serialize(customerJsonModel, options);

                // Rewrite the updated file.
                System.IO.File.WriteAllText(customersFileName, serializedCustomers);

                // Return the deleted customer.
                return Ok(deleteCustomer);
            }
            catch (Exception e)
            {
                // return an error message with a 500 Server Error HTTP Code.
                return StatusCode(500, e.Message);
            }
        }
    }
}
