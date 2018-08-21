using System;
using Newtonsoft.Json;

namespace Tobii.Build.Robot.Model
{
    public class Change : RestBase
    {
        public string Id { get; set; }
        public string Version { get; set; }

        [JsonConverter(typeof(TeamcityDateTimeConverter), Default.DateFormat)]
        public DateTime Date { get; set; }
        public string Username { get; set; }
    }
}