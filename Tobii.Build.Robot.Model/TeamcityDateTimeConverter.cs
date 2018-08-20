using Newtonsoft.Json.Converters;

namespace Tobii.Build.Robot.Model
{
    public class TeamcityDateTimeConverter : IsoDateTimeConverter
    {

        public TeamcityDateTimeConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}