using System;
using System.Text.Json.Serialization;

namespace JohnDoeAgentMgmtSystem.Models
{
    public class Customer
    {
        [JsonPropertyName("_id")]
        public int Id { get; set; }
        [JsonPropertyName("agent_id")]
        public int AgentId { get; set; }
        public Guid Guid { get; set; }
        public bool IsActive { get; set; }
        public string Balance { get; set; }
        public int Age { get; set; }
        public string EyeColor { get; set; }
        public CustomerName Name { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Registered { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string[] Tags { get; set; }
    }
}
