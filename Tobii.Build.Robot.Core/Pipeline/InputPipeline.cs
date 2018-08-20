using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Tobii.Build.Robot.Core
{
    public class InputPipeline
    {
        BlockingCollection<Message> blockingCollection = new BlockingCollection<Message>();

        public bool TryGetMessage(out Message item)
        {
            while(blockingCollection.Count > 0)
            {
                if(blockingCollection.TryTake(out item))
                {
                    return true;
                }
            }
            return WaitForLine(out item);
        }

        bool WaitForLine(out Message message)
        {
            message = null;
            if(Reader.TryReadLine(out string line, 50))
            {
                message = new Message()
                {
                    Content = line,
                    Source = this.GetType().Name
                };
                return true;
            }
            return false;
        }

        public void Enqueue(Message message)
        {
            blockingCollection.Add(message);
        }

        class Reader
        {
            private static Thread inputThread;
            private static AutoResetEvent getInput, gotInput;
            private static string input;

            static Reader()
            {
                getInput = new AutoResetEvent(false);
                gotInput = new AutoResetEvent(false);
                inputThread = new Thread(reader);
                inputThread.IsBackground = true;
                inputThread.Start();
            }

            private static void reader()
            {
                while (true)
                {
                    getInput.WaitOne();
                    input = Console.ReadLine();
                    gotInput.Set();
                }
            }

            // omit the parameter to read a line without a timeout
            public static bool TryReadLine(out string line, int timeOutMillisecs = Timeout.Infinite)
            {
                line = "";
                getInput.Set();
                bool success = gotInput.WaitOne(timeOutMillisecs);
                if (success)
                    line = input;
                return success;
            }
        }
    }
}