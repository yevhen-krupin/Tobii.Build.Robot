using System;

namespace Tobii.Build.Robot.Config
{
    public class ConfigurationException : Exception
    {
        internal ConfigurationException(string message) : base(message)
        {
        }
    }
}
