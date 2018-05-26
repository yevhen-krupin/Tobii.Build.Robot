using System.Collections.Generic;

namespace Tobii.Build.Robot.Model
{
    public class Changes : RestBase
    {
        public int Count { get; set; }
        public List<Change> change { get; set; }
    }
}