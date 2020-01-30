using System.Net;
using DnsClient;
using Microsoft.AspNetCore.Mvc;

namespace DnsLookupService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReverseDnsController : ControllerBase
    {
        private LookupClient _lookupClient;

        public ReverseDnsController()
        {
            _lookupClient = new LookupClient();
        }

        [HttpGet]
        public string Get(string ipAddress)
        {
            var isIpAddress = IPAddress.TryParse(ipAddress, out IPAddress address);

            if (!isIpAddress)
                return ipAddress; //If it's a domain name, we can just return that directly

            return _lookupClient.GetHostName(address);
        }
    }
}
