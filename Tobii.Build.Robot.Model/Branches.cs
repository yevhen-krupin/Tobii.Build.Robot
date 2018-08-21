using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tobii.Build.Robot.Model
{
    public class Branches : RestBase
    {
        public List<Branch> Branch { get; set; }
        public string Count { get; set; }
    }
}