using System.Linq;
using Tobii.Build.Robot.Config;

namespace Tobii.Build.Robot.Telegram
{
    public class ConfigurationProvider : ConfigurationProviderBase
    {
        public const string ApiKeyVariableName = "TELEGRAM_BOT_API_KEY_VARIABLE";
        public const string BotNameVariableName = "BOT_NAME_VARIABLE";

        public string ApiKey { get; }
        public string BotName { get; }

        public ConfigurationProvider(string configPath = DefaultIniFile) : base(configPath)
        {
            var definitions = new[] {ApiKeyVariableName, BotNameVariableName};
            var dictionary = LoadConfig(definitions);
            var variables = LoadVariables(definitions.Select(x => dictionary[x]));
            ApiKey = variables[dictionary[ApiKeyVariableName]];
            BotName = variables[dictionary[BotNameVariableName]];
        }
    }
}