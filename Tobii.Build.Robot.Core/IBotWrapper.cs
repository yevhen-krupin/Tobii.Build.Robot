using System;

namespace Tobii.Build.Robot.Core
{
    public interface IBotWrapper : IDisposable
    {
        void Start();
    }
}