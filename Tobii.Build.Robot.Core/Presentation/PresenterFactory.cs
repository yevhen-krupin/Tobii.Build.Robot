using Tobii.Build.Robot.Core.Route;

namespace Tobii.Build.Robot.Core
{
    public class PresenterFactory : IPresenterFactory
    {
        public IOutputView Text(string message)
        {
            return TextView.FromMessage(message);
        }

        public IOutputView Options(string title, Clickable[] options)
        {
            return OptionsView.FromOptions(title, options);
        }
    }
}