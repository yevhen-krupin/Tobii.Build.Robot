using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Tobii.Build.Robot.Model
{
    public class BuildType : Entity
    {
        public Project Project { get; set; }
        public Builds Builds { get; set; }
        public Properties Settings { get; set; }
        public Properties Parameters { get; set; }
        [JsonProperty("agent-requirements")]
        public AgentRequirements AgentRequirements { get; set; }
    }
}