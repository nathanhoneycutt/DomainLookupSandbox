using System.Linq;
using Microsoft.AspNetCore.Mvc;
using IpStack;
using DomainLookupCore.SharedModels;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace GeolocationLookupService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeolocationController : ControllerBase
    {
        private readonly string _ipStackApiKey;

        public GeolocationController(IConfiguration configuration)
        {
            _ipStackApiKey = configuration.GetValue<string>("DOMAINLOOKUP_IPSTACKKEY");
        }

        [HttpGet]
        public GeolocationDetails Get(string domain)
        {
            var client = new IpStackClient(_ipStackApiKey);
            var details = client.GetIpAddressDetails(domain);
            var returnDetails = new GeolocationDetails();

            var ipStackProperties = details.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var returnProp in returnDetails.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var ipStackProp = ipStackProperties.FirstOrDefault(x => x.Name == returnProp.Name && x.PropertyType == returnProp.PropertyType);

                if (ipStackProp == null)
                    continue;

                returnProp.SetValue(returnDetails, ipStackProp.GetValue(details));
            }

            return returnDetails;            
        }
    }
}
