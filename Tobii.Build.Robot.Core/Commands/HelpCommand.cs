using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tobii.Build.Robot.Core.Commands
{
    public class HelpCommand : CommandBase
    {
        private readonly IEnumerable<CommandBase> _commands;

        public HelpCommand(IEnumerable<CommandBase> commands, CancellationTokenSource cancellationTokenSource) : base(cancellationTokenSource)
        {
            _commands = commands;
        }

        public override string Name { get { return "help"; } }

        public override async Task Do(Output output, string[] parameters)
        {
            var commands = _commands
                .Select(x => x.Name)
                .Concat(new []{Name})
                .OrderBy(x => x, StringComparer.OrdinalIgnoreCase);
            var commandsList = string.Join(Environment.NewLine + " - ", commands.ToArray());
            output.Write("Available commands: " + Environment.NewLine + commandsList);
        }
    }
}