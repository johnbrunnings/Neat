using System.Collections.Generic;

namespace Neat.Infrastructure.Net
{
    public interface IIPAddressProvider
    {
        string GetCurrentRequestIPAddress();
        string GetCurrentMachineIPAddress(bool getIpv6 = false);
        IEnumerable<string> GetCurrentMachineIPAddresses(bool getIpv6 = false);
    }
}