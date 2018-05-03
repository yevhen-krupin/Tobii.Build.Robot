using System.IO;

namespace Tobii.Build.Robot.Core
{
    public class FileStream : IOutputStream
    {
        private readonly string _file;
        private readonly object _lock = new object();

        public FileStream(string file)
        {
            _file = file;
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