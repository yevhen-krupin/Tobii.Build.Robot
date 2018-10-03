using System;

namespace Tobii.Build.Robot.Core.Route
{
    public class Clickable
    {
        public Clickable(string name, string from, string fromId, string to, string toId)
        {
            Name = name;
            To = to;
            ToData = toId;
            From = from + " " + fromId;
            Id = Guid.NewGuid().ToString("N");
        }

        public string Name { get; set; }

        public string To { get; set; }
        
        public string ToData { get; set; }

        public string From { get; set; }

        public string Id { get; }
    }
}