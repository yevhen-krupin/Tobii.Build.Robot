using System.Collections.Generic;
using System.Linq;

namespace Tobii.Build.Robot
{
    public class ExecutionContext
    {
        private readonly IEnumerable<CommandBase> _commands;
        private readonly Output _output;

        public ExecutionContext(IEnumerable<CommandBase> commands, Output output)
        {
            _commands = commands;
            _output = output;
        }
        
        public void Execute(string name)
        {
            var command = _commands.SingleOrDefault(x => x.Name == name);
            if (command != null)
            {
                command.Do();
            }
            else
            {
                _output.Write("command not found");
            }
        }
    }
}