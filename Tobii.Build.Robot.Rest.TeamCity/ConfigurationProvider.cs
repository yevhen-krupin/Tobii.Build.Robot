using System.Linq;
using Tobii.Build.Robot.Config;

namespace Tobii.Build.Robot.Rest.TeamCity
{
    public class ConfigurationProvider : ConfigurationProviderBase
    {
        public const string LoginVariableName = "TEAMCITY_LOGIN_VARIABLE";
        public const string PasswordVariableName = "TEAMCITY_PASSWORD_VARIABLE";
        public const string HostVariableName = "TEAMCITY_HOST_VARIABLE";

        public string Login { get; }
        public string Password { get; }
        public string Host { get; }

        public ConfigurationProvider(string configPath = DefaultIniFile) : base(configPath)
        {
            var definitions = new[] { LoginVariableName, PasswordVariableName, HostVariableName };
            var dictionary = LoadConfig(definitions);
            var variables = LoadVariables(definitions.Select(x => dictionary[x]));
            Login = variables[dictionary[LoginVariableName]];
            Password = variables[dictionary[PasswordVariableName]];
            Host = variables[dictionary[HostVariableName]];
        }
    }
}