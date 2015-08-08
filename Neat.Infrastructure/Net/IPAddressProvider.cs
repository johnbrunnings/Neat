using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;

namespace Neat.Infrastructure.Net
{
    public class IPAddressProvider : IIPAddressProvider
    {
        public string GetCurrentRequestIPAddress()
        {
            var ipAddress = "127.0.0.1";
            var ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            var proxyIps = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            var proxyIpList = !string.IsNullOrEmpty(proxyIps) ? proxyIps.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries) : new string[0];
            var proxyIp = proxyIpList.FirstOrDefault();

            ipAddress = proxyIp ?? ip ?? ipAddress;

            return ipAddress;
        }

        public string GetCurrentMachineIPAddress(bool getIpv6 = false)
        {
            var ipAddress = "";
            var addressFamily = getIpv6 ? AddressFamily.InterNetworkV6 : AddressFamily.InterNetwork;

            for(var i = 0; i < Dns.GetHostAddresses(Dns.GetHostName()).Length; i++)
            {
                var ipAddressInfo = Dns.GetHostAddresses(Dns.GetHostName())[i];
                if (ipAddressInfo.AddressFamily == addressFamily)
                {
                    ipAddress = ipAddressInfo.ToString();
                    break;
                }
            }

            return ipAddress;
        }

        public IEnumerable<string> GetCurrentMachineIPAddresses(bool getIpv6 = false)
        {
            var ipAddress = new List<string>();
            var addressFamily = getIpv6 ? AddressFamily.InterNetworkV6 : AddressFamily.InterNetwork;

            for (var i = 0; i < Dns.GetHostAddresses(Dns.GetHostName()).Length; i++)
            {
                var ipAddressInfo = Dns.GetHostAddresses(Dns.GetHostName())[i];
                if (ipAddressInfo.AddressFamily == addressFamily)
                {
                    ipAddress.Add(ipAddressInfo.ToString());
                }
            }

            return ipAddress;
        }
    }
}