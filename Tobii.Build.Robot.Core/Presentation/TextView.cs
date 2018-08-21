using System;
using System.Text;

namespace Tobii.Build.Robot.Core
{
    public class TextView : IOutputView
    {
        private string _richMessage;

        private TextView(string message)
        {
            var sb = new StringBuilder("[").Append(DateTime.Now.ToString("G")).Append("] ").Append(message);
            _richMessage = sb.ToString();
        }

        public static TextView FromMessage(string message)
        {
            return new TextView(message);
        }
        
        public void Present(ITextStream stream)
        {
            stream.Write(_richMessage);
        }

        public void Present(IUIStream stream)
        {
            stream.ShowMessage(_richMessage);
        }
    }
}