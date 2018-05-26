using System.Xml.Serialization;

namespace Tobii.Build.Robot.Model
{
    public class Branch : RestBase
    {
        public string Name { get; set; }
        public string Default { get; set; }
    }
}