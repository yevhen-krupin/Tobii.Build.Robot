using System.Xml.Serialization;

namespace Tobii.Build.Robot.Model
{
    public class Project : RestBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentProjectId { get; set; }
        public string WebUrl { get; set; }
    }
}