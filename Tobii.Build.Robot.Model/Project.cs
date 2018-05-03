using System.Xml.Serialization;

namespace Tobii.Build.Robot.Model
{
    public class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentProjectId { get; set; }
        public string Href { get; set; }
        public string WebUrl { get; set; }
    }
}