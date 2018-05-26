using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Xml.Serialization;

namespace Tobii.Build.Robot.Model
{
    public class Build : RestBase
    {
        public string Id { get; set; }
        public string BuildTypeId { get; set; }
        public string State { get; set; }
        public string Number { get; set; }
        public string Status { get; set; }
        public string StatusText { get; set; }
        public BuildType BuildType { get; set; }
        [JsonConverter(typeof(TeamcityDateTimeConverter), "yyyyMMdd'T'HHmmssK")]
        public DateTime QueuedDate { get; set; }

        [JsonConverter(typeof(TeamcityDateTimeConverter), "yyyyMMdd'T'HHmmssK")]
        public DateTime StartDate { get; set; }

        [JsonConverter(typeof(TeamcityDateTimeConverter), "yyyyMMdd'T'HHmmssK")]
        public DateTime FinishDate { get; set; }
        public Changes Changes { get; set; }
        public Revisions Revisions { get; set; }
    }

    public class TeamcityDateTimeConverter : IsoDateTimeConverter
    {
        public TeamcityDateTimeConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}