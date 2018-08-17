using System.IO;

namespace Tobii.Build.Robot.Core
{
    public class FileStream : IOutputStream, ITextStream
    {
        private readonly string _file;
        private readonly object _lock = new object();

        public FileStream(string file)
        {
            _file = file;
        }

        public void Show(IOutputView textView)
        {
            textView.Present(this);
        }

        public void Write(string message)
        {
            lock (_lock)
            {
                File.AppendAllLines(_file, new[] {message});
            }
        }
    }
}