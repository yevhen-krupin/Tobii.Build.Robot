using System;

namespace Tobii.Build.Robot.Core
{
    public class CommandNotFoundException : Exception
    {
        public CommandNotFoundException() : base("command not found")
        {
        }
    }
}