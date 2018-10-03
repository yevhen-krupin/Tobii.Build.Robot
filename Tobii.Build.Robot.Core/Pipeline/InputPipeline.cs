using System.Collections.Concurrent;

namespace Tobii.Build.Robot.Core.Pipeline
{
    public class InputPipeline
    {
        readonly BlockingCollection<Message> _blockingCollection = new BlockingCollection<Message>();

        public bool TryGetMessage(out Message item)
        {
            item = null;
            while (_blockingCollection.Count > 0)
            {
                if(_blockingCollection.TryTake(out item))
                {
                    return true;
                }
            }

            return false;
        }

        public void Enqueue(Message message)
        {
            _blockingCollection.Add(message);
        }
    }
}