using System;

namespace Tobii.Build.Robot.Core
{
    public class Clickable
    {
        public Clickable(string name, string from, string fromId, string to, string toId)
        {
            Name = name;
            To = to + " " + toId;
            From = from + " " + fromId;
        }

        public string Name { get; set; }

        public string To { get; set; }
        
        public string From { get; set; }
    }
}