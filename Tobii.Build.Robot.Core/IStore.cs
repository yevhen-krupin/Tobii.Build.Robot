using System;
using System.Collections.Generic;
using System.Text;

namespace Tobii.Build.Robot.Core
{
    public interface IStore
    {
        void Put<T>(string user, string id, T item);
        T Get<T>(string chatId, string id);
        void Remove(string chatId, string id);
    }
}
