using System.Collections.Generic;
using System.Linq;

namespace Tobii.Build.Robot.Core
{
    public class Output : IOutputStream
    {
        private readonly IList<IOutputStream> _streams;

        public IPresenterFactory PresenterFactory { get; }

        public Output(IPresenterFactory presenterFactory, IEnumerable<IOutputStream> streams)
        {
            _streams = streams.ToList();
            PresenterFactory = presenterFactory;
        }

        public void Write(string text)
        {
            Show(PresenterFactory.Text(text));
        }

        public void Show(IOutputView outputView)
        {
            foreach (var outputStream in _streams)
            {
                outputStream.Show(outputView);
            }
        }
    }
}