using System;
using System.IO;

namespace Tobii.Build.Robot
{
    public class Output
    {
        private readonly object _lock = new object();

        public void Write(string message)
        {
            Console.WriteLine(message);
            lock (_lock)
            {
                File.WriteAllText("log.txt", message);
            }
        }
    }
}