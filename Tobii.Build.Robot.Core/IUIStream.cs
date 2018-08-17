namespace Tobii.Build.Robot.Core
{
    public interface IUIStream
    {
        void ShowMessage(string richMessage);
        void ShowButton(Clickable button);
        void ShowOptions(string title, Clickable[] button);
    }
}