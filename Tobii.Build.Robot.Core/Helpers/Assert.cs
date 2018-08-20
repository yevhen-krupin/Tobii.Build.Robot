using System.Collections.Generic;
using System.Linq;

namespace Tobii.Build.Robot.Core
{
    public static class Assert
    {
        public static void Count<T>(IEnumerable<T> enumerable, int count)
        {
            if(enumerable.Count() != count)
            {
                throw new System.ArgumentException("Enumerable count is not expected");
            }
        }
    }
}