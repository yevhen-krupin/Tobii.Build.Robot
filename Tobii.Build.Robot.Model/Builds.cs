using System.Xml.Serialization;

namespace Tobii.Build.Robot.Model
{
    public class Builds : RestBase
    {
        public Build[] Build { get; set; }
    }
}