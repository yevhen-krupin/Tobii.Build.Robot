using System;
using Newtonsoft.Json;

namespace Tobii.Build.Robot.Model
{
    public class Comment : RestBase
    {
        public User User { get; set; }

        [JsonConverter(typeof(TeamcityDateTimeConverter), Default.DateFormat)]
        public DateTime Timestamp { get; set; }

        public string Text { get; set; }
    }
}