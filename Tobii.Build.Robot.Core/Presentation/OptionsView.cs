using Tobii.Build.Robot.Core.Route;

namespace Tobii.Build.Robot.Core
{
    public class OptionsView : IOutputView
    {
        private string _title;
        private Clickable[] _options;

        public static OptionsView FromOptions(string title, Clickable[] options)
        {
            return new OptionsView(title, options);
        }

        private OptionsView(string title, Clickable[] options)
        {
            _title = title;
            _options = options;
        }

        public void Present(ITextStream stream)
        {
            stream.Write(_title);
            foreach (var option in _options)
            {
                stream.Write($"{option.Name}: {option.To}");
            }
        }

        public void Present(IUIStream stream)
        {
            stream.ShowOptions(_title, _options);
        }
    }
}