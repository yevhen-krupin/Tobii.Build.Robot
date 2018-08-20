using System;

namespace Tobii.Build.Robot.Core
{
    public class ConsoleStream : ITextStream, IOutputStream
    {
        public void Show(IOutputView textView)
        {
            textView.Present(this);
        }

        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}