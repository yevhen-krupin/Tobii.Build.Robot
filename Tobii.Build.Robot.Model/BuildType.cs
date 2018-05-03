using System.Xml.Serialization;

namespace Tobii.Build.Robot.Model
{
    public class BuildType
    {
        public Project Project { get; set; }
        public Builds Builds { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}