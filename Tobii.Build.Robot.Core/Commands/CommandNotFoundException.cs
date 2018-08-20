using System;

namespace Tobii.Build.Robot.Core.Commands
{
    public class CommandNotFoundException : Exception
    {
        public CommandNotFoundException() : base("command not found")
        {
        }
    }
}