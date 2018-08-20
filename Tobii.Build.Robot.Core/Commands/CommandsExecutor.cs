using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tobii.Build.Robot.Core.Commands
{
    public class CommandsExecutor
    {
        public IList<CommandBase> Commands { get; }
        
        public CommandsExecutor(IEnumerable<CommandBase> commands)
        {
            Commands = commands.ToList();
        }
        
        public async Task Execute(string message, Output output)
        {
            var splitted = message.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var command = Commands.SingleOrDefault(x => x.Name.Equals(splitted.First(), StringComparison.OrdinalIgnoreCase));
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