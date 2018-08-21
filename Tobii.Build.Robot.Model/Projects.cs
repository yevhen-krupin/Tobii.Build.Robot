using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tobii.Build.Robot.Model
{
    public class Projects : RestBase
    {
        public int Count { get; set; }
        public List<Project> Project { get; set; }
    }
}