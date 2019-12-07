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

        // POST: api/Agents
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

                var isFound = agentJsonModel.Exists(agent => agent.Id == updateAgent.Id);

                if (!isFound)
                {
                    // If there is already an agent with the specified ID, return a 400 HTTP code and an error message.
                    return BadRequest($"Unable to update: There is no agent with ID {value.Id}");
                }

                // use a foreach loop to find the specified agent by ID, update the data, and then exit the loop
                // (rather than use LINQ).
                foreach (var t in agentJsonModel)
                {
                    // If it's not the specified agent, go on to the next.
                    if (t.Id != updateAgent.Id) continue;

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

                // Return the inserted agent.
                return Ok(updateAgent);
            }
            catch (Exception e)
            {
                // return an error message with a 500 Server Error HTTP Code.
                return StatusCode(500, e.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Return a 500 error code as deleting agents was not called for.
            // Return a StatusCode object rather than a NotImplementedException for security reasons (do not provide stack trace).
            return StatusCode(500, "Agent deletion will not be implemented for this exercise.");
        }
    }
}
