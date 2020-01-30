using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Mvc;

namespace PingLookupService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController : ControllerBase
    {
        private readonly Ping _ping;

        public PingController()
        {
            _ping = new Ping();
        }

        [HttpGet]
        public long Get(string domain)
        {
            var reply = _ping.Send(domain);

            if (reply.Status == IPStatus.Success)
                return reply.RoundtripTime;

            return -1;
        }
    }
}
