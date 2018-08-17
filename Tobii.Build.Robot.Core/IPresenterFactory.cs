namespace Tobii.Build.Robot.Core
{
    public interface IPresenterFactory
    {
        IOutputView Text(string message);
    }
}