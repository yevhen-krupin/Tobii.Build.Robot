using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tobii.Build.Robot.Core
{
    public class CommandsExecutor
    {
        private readonly IEnumerable<CommandBase> _commands;

        public CommandsExecutor(IEnumerable<CommandBase> commands)
        {
            _commands = commands;
        }
        
        public async Task Execute(string message, Output output)
        {
            var splitted = message.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var command = Enumerable.SingleOrDefault(_commands, x => x.Name == splitted.First());
            if (command != null)
            {
                var parameters = splitted.Skip(1).ToArray();
                await command.Do(output, parameters);
            }
            else
            {
                throw new CommandNotFoundException();
            }
        }
    }
}