using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tobii.Build.Robot.Config
{
    public abstract class ConfigurationProviderBase
    {
        public const string DefaultIniFile = "bot.ini";
        public readonly string IniFile;

        protected ConfigurationProviderBase(string configPath = DefaultIniFile)
        {
            IniFile = configPath;
        }

        protected IDictionary<string, string> LoadConfig(IEnumerable<string> definitions)
        {
            if (!File.Exists(IniFile))
            {
                Environment.FailFast("'" + IniFile + "' file not found.");
            }
            var content = File.ReadAllLines(IniFile);
            var list = content.Select(x => x.Split("=", StringSplitOptions.RemoveEmptyEntries)).ToList();
            var dictionary = list.ToDictionary(x => x[0], x => x[1]);
            ValidateConfig(dictionary, definitions);
            return dictionary;
        }

        protected IDictionary<string,string> LoadVariables(IEnumerable<string> names)
        {
            var variables = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.User);
            ValidateVariables(variables, names);
            var dictionary = new Dictionary<string, string>();

            foreach (var variable in variables.Keys)
            {
                var key = variable.ToString();
                dictionary.Add(key, variables[key].ToString());
            }
            return dictionary;
        }

        protected void ValidateConfig(IDictionary<string, string> dictionary, IEnumerable<string> definitions)
        {
            foreach (var definition in definitions)
            {
                if (!dictionary.ContainsKey(definition))
                {
                    throw new ConfigurationException("bot.ini file does not contain " + definition + " definition.");
                }
            }
        }

        protected void ValidateVariables(IDictionary variables, IEnumerable<string> names)
        {
            foreach (var variable in names)
            {
                if (!variables.Contains(variable))
                {
                    throw new ConfigurationException(variable + " environment variable not found.");
                }
            }
        }
    }
}