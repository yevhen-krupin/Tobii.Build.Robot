using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tobii.Build.Robot.Core
{
    public class Output : IOutputStream
    {
        private readonly IList<IOutputStream> _streams;

        public Output(IEnumerable<IOutputStream> streams)
        {
            _streams = streams.ToList();
        }

        public void Write(string message)
        {
            var sb = new StringBuilder("[").Append(DateTime.Now.ToString("G")).Append("] ").Append(message);
            message = sb.ToString();
            foreach (var outputStream in _streams)
            {
                outputStream.Write(message);
            }
        }
    }
}