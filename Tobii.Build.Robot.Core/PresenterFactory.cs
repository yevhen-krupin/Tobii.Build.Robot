namespace Tobii.Build.Robot.Core
{
    public class PresenterFactory : IPresenterFactory
    {
        public IOutputView Text(string message)
        {
            return TextView.FromMessage(message);
        }
    }
}