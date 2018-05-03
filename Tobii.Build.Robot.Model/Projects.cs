using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tobii.Build.Robot.Model
{
    public class Projects
    {
        public int count { get; set; }
        public string href { get; set; }
        public List<Project> project { get; set; }
    }
}