using System;

namespace Tobii.Build.Robot.Core
{
    public class Clickable
    {
        public Clickable(string name, Action callback)
        {
            Name = name;
            Callback = callback;
        }

        public string Name { get; set; }

        public Action Callback { get; set; }
    }
}