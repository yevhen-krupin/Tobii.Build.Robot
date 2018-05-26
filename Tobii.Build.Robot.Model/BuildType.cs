using System.Xml.Serialization;

namespace Tobii.Build.Robot.Model
{
    public class BuildType : RestBase
    {
        public Project Project { get; set; }
        public Builds Builds { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}