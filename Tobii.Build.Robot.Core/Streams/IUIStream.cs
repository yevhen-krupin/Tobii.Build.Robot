using Tobii.Build.Robot.Core.Route;

namespace Tobii.Build.Robot.Core
{
    public interface IUIStream
    {
        void ShowMessage(string richMessage);
        void ShowOptions(string title, Clickable[] button);
    }
}