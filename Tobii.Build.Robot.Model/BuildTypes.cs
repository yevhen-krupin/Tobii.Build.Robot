using System.Xml.Serialization;

namespace Tobii.Build.Robot.Model
{
    public class BuildTypes : RestBase
    {
        public BuildType[] BuildType { get; set; }
    }
}