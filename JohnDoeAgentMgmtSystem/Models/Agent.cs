using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JohnDoeAgentMgmtSystem.Models
{
    public class Agent
    {
        [JsonPropertyName("_id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int Tier { get; set; }
        public AgentPhone Phone { get; set; }
    }
}
