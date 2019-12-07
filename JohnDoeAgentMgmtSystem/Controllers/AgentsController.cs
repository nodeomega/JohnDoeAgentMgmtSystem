using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using JohnDoeAgentMgmtSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace JohnDoeAgentMgmtSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        // GET: api/Agents
        /// <summary>
        /// Gets the listing of all Agents.
        /// </summary>
        /// <returns>A list of all Agents.</returns>
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
                var agentJsonFileString = System.IO.File.ReadAllText(@"Data\agents.json");

                // Deserialize the JSON into a list of Agents.
                var agentJsonModel = JsonSerializer.Deserialize<List<Agent>>(agentJsonFileString, options);

                // Return the list of Agents.
                return Ok(agentJsonModel);
            }
            catch (Exception e)
            {
                // return an error message with a 500 Server Error HTTP Code.
                return StatusCode(500, e.Message);
            }
        }

        // GET: api/Agents/5
        /// <summary>
        /// Gets the specified agent.
        /// </summary>
        /// <param name="id">The specified Agent's ID'</param>
        /// <returns>A single Agent's data.'</returns>
        [HttpGet("{id}", Name = "Get[controller]")]
        public IActionResult Get(int id)
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
                var agentJsonFileString = System.IO.File.ReadAllText(@"Data\agents.json");

                // Deserialize the JSON into a list of Agents.
                var agentJsonModel = JsonSerializer.Deserialize<List<Agent>>(agentJsonFileString, options);
                var singleAgent = agentJsonModel.FirstOrDefault(agent => agent.Id == id);

                if (singleAgent == null)
                {
                    return NotFound($"The specified agent with ID {id} was not found.");
                }

                // Return the Agent.
                return Ok(singleAgent);
            }
            catch (Exception e)
            {
                // return an error message with a 500 Server Error HTTP Code.
                return StatusCode(500, e.Message);
            }
        }

        // GET api/Agents/5/customers
        /// <summary>
        /// Gets the list of Customers under a specific Agent's ID.
        /// </summary>
        /// <param name="id">The Agent's ID</param>
        /// <returns>A List of Customers belonging to the Agent.</returns>
        [HttpGet("{id}/customers", Name = "Get[controller]Customers")]
        public IActionResult GetAgentCustomers(int id)
        {
            // Set JSON serializer options.
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            try
            {
                var agentsFileName = @"Data\agents.json";
                var customersFileName = @"Data\customers.json";

                // get the JSON file text.
                var agentJsonFileString = System.IO.File.ReadAllText(agentsFileName);

                // Deserialize the JSON into a list of Agents.
                var agentJsonModel = JsonSerializer.Deserialize<List<Agent>>(agentJsonFileString, options);
                var singleAgent = agentJsonModel.FirstOrDefault(agent => agent.Id == id);

                // If the agent isn't found, exit with a 404 error (agent not found)
                if (singleAgent == null)
                {
                    return NotFound($"The specified agent with ID {id} was not found.");
                }

                // Get the listing of Customers under this agent.
                var customerJsonFileString = System.IO.File.ReadAllText(customersFileName);

                var customerJsonModel = JsonSerializer.Deserialize<List<Customer>>(customerJsonFileString, options)
                    .Where(customer => customer.AgentId == singleAgent.Id).ToList();

                // Return the Agent's Customers.
                return Ok(customerJsonModel);
            }
            catch (Exception e)
            {
                // return an error message with a 500 Server Error HTTP Code.
                return StatusCode(500, e.Message);
            }
        }

        // POST: api/Agents
        /// <summary>
        /// Creates a new Agent
        /// </summary>
        /// <param name="value">The new Agent's data in JSON format.</param>
        /// <returns>The inserted Agent's data.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] Agent value)
        {
            // Set JSON serializer options.
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var fileName = @"Data\agents.json";

            try
            {
                // get the JSON file text.
                var agentJsonFileString = System.IO.File.ReadAllText(fileName);

                // Deserialize the JSON into a list of Agents.
                var agentJsonModel = JsonSerializer.Deserialize<List<Agent>>(agentJsonFileString, options);

                // get the values for the new agent.
                var newAgent = value;

                var isDuplicateId = agentJsonModel.Exists(agent => agent.Id == newAgent.Id);

                if (isDuplicateId)
                {
                    // If there is already an agent with the specified ID, return a 400 HTTP code and an error message.
                    return BadRequest($"There is already an agent with ID {value.Id}");
                }
                
                // Add the agent to the list of agents.
                agentJsonModel.Add(newAgent);

                // serialize the updated list of Agents.
                var serializedAgents = JsonSerializer.Serialize(agentJsonModel, options);

                // Rewrite the updated file.
                System.IO.File.WriteAllText(fileName, serializedAgents);

                // Return the inserted agent.
                return Ok(newAgent);
            }
            catch (Exception e)
            {
                // return an error message with a 500 Server Error HTTP Code.
                return StatusCode(500, e.Message);
            }
        }

        // PUT: api/Agents/5
        /// <summary>
        /// Updates an Agent.
        /// </summary>
        /// <param name="id">The Agent's ID.'</param>
        /// <param name="value">The updated Agent data in JSON format.</param>
        /// <returns>The updated Agent data.</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Agent value)
        {
            // Set JSON serializer options.
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var fileName = @"Data\agents.json";

            try
            {
                // get the JSON file text.
                var agentJsonFileString = System.IO.File.ReadAllText(fileName);

                // Deserialize the JSON into a list of Agents.
                var agentJsonModel = JsonSerializer.Deserialize<List<Agent>>(agentJsonFileString, options);

                // get the values for the new agent.
                var updateAgent = value;

                var isFound = agentJsonModel.Exists(agent => agent.Id == id);

                if (!isFound)
                {
                    // If the agent with the specified ID doesn't exist, return a 400 HTTP code and an error message.
                    return BadRequest($"Unable to update: There is no agent with ID {value.Id}");
                }

                // use a foreach loop to find the specified agent by ID, update the data, and then exit the loop
                // (rather than use LINQ).
                foreach (var t in agentJsonModel)
                {
                    // If it's not the specified agent, go on to the next.
                    if (t.Id != updateAgent.Id) continue;

                    // I did not implement changing of Agent ID for this exercise to maintain data integrity re: Agent -> Customer

                    t.Address = updateAgent.Address;
                    t.City = updateAgent.City;
                    t.Name = updateAgent.Name;
                    t.Phone = updateAgent.Phone;
                    t.State = updateAgent.State;
                    t.Tier = updateAgent.Tier;
                    t.ZipCode = updateAgent.ZipCode;
                    break;
                }

                // serialize the updated list of Agents.
                var serializedAgents = JsonSerializer.Serialize(agentJsonModel, options);

                // Rewrite the updated file.
                System.IO.File.WriteAllText(fileName, serializedAgents);

                // Return the updated agent.
                return Ok(updateAgent);
            }
            catch (Exception e)
            {
                // return an error message with a 500 Server Error HTTP Code.
                return StatusCode(500, e.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Placeholder for Agent deletion.  Not Implemented.
        /// </summary>
        /// <param name="id">An Agent ID.</param>
        /// <returns>Currently generates a 500 Server Error.</returns>
        /// <remarks>Included for if Agent deletion is implemented in the future.</remarks>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Return a 500 error code as deleting agents was not called for.
            // Return a StatusCode object rather than a NotImplementedException for security reasons (do not provide stack trace).
            return StatusCode(500, "Agent deletion will not be implemented for this exercise.");
        }
    }
}
