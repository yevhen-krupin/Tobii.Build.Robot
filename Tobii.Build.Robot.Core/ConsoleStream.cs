using System;

namespace Tobii.Build.Robot.Core
{
    public class ConsoleStream : IOutputStream
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}