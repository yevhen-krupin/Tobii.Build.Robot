using System.Collections.Generic;
using System.Linq;

namespace Tobii.Build.Robot.Rest.Core
{
    public class Gateway
    {
        private readonly IEnumerable<IGateway> gateways;

        public Gateway(IEnumerable<IGateway> gateways)
        {
            this.gateways = gateways;
        }

        public T For<T>() where T : IGateway
        {
            return gateways.OfType<T>().Single();
        }
    }
}
