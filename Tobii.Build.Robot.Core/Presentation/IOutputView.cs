namespace Tobii.Build.Robot.Core
{
    public interface IOutputView
    {
        void Present(ITextStream stream);
        void Present(IUIStream stream);
    }
}