using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tobii.Build.Robot
{
    public class ConfigurationProvider
    {
        public const string ApiKeyVariableName = "TELEGRAM_BOT_API_KEY_VARIABLE";
        public const string BotNameVariableName = "BOT_NAME_VARIABLE";

        public const string IniFile = "bot.ini";
        public string ApiKey { get; }
        public string BotName { get; }

        public ConfigurationProvider()
        {
            if (!File.Exists(IniFile))
            {
                Environment.FailFast("'bot.ini' file not found.");
            }
            var content = File.ReadAllLines(IniFile);
            var list = content.Select(x => x.Split("=", StringSplitOptions.RemoveEmptyEntries)).ToList();
            var dictionary = list.ToDictionary(x => x[0], x => x[1]);
            validateConfig(dictionary);

            var variables = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.User);
            validateVariables(variables, dictionary);

            ApiKey = Environment.GetEnvironmentVariable(dictionary[ApiKeyVariableName], EnvironmentVariableTarget.User);
            BotName = Environment.GetEnvironmentVariable(dictionary[BotNameVariableName], EnvironmentVariableTarget.User);
        }

        private void validateConfig(IDictionary<string, string> dictionary)
        {
            if (!dictionary.ContainsKey(ApiKeyVariableName))
            {
                throw new ConfigurationException("bot.ini file does not contain " + ApiKeyVariableName + " definition.");
            }
            if (!dictionary.ContainsKey(BotNameVariableName))
            {
                throw new ConfigurationException("bot.ini file does not contain " + BotNameVariableName + " definition.");
            }
        }

        private void validateVariables(IDictionary dictionary, IDictionary<string, string> config)
        {
            if (!dictionary.Contains(config[ApiKeyVariableName]))
            {
                throw new ConfigurationException(config[ApiKeyVariableName]+ " environment variable not found.");
            }
            if (!dictionary.Contains(config[BotNameVariableName]))
            {
                throw new ConfigurationException(config[BotNameVariableName] + " environment variable not found.");
            }
        }
    }

    public class ConfigurationException : Exception
    {
        internal ConfigurationException(string message) : base(message)
        {
        }
    }
}