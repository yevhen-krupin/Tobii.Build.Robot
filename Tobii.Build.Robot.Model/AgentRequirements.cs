using System.Collections.Generic;
using Newtonsoft.Json;

namespace Tobii.Build.Robot.Model
{
    public class AgentRequirements
    {
        public int Count { get; set; }

        [JsonProperty("agent-requirement")]
        public List<AgentRequirement> AgentRequirement { get; set; }
    }
}