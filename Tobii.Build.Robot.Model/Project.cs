using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tobii.Build.Robot.Model
{
    public class Project : Entity
    {
        public Project ParentProject { get; set; }
        public string ParentProjectId { get; set; }
        public string WebUrl { get; set; }
        public BuildTypes BuildTypes { get; set; }
        public Projects Projects { get; set; }
    }
}