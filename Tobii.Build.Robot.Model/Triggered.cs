using System;
using Newtonsoft.Json;

namespace Tobii.Build.Robot.Model
{
    public class Triggered
    {
        public string Type { get; set; }

        [JsonConverter(typeof(TeamcityDateTimeConverter), Default.DateFormat)]
        public DateTime Date { get; set; }

        public User User { get; set; }
    }
}