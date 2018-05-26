using System;

namespace Tobii.Build.Robot.Model
{
    public class Change : RestBase
    {
        public string Id { get; set; }
        public string Version { get; set; }
        public DateTime Date { get; set; }
        public string Username { get; set; }
    }
}