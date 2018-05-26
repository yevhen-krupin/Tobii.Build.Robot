using System.Collections.Generic;

namespace Tobii.Build.Robot.Model
{
    public class Revisions : RestBase
    {
        public int Count { get; set; }
        public List<Revision> Revision { get; set; }
    }
}