using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DomainLookupApi.Models.DomainInfo;
using DomainLookupCore.Validation;
using DomainLookupCore.SharedModels;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace DomainLookupApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DomainInfoController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly List<IValidateDomains> _domainValidators;
        private readonly string _geolocationUrl;
        private readonly string _pingUrl;
        private readonly string _reverseDnsUrl;
        
        public DomainInfoController(IConfiguration configuration)
        {
            _domainValidators = new List<IValidateDomains>(new []
            {
                new IpAddressValidator() as IValidateDomains,
                new DomainNameValidator() as IValidateDomains
            });

            _httpClient = new HttpClient();
            _geolocationUrl = configuration.GetValue<string>("DOMAINLOOKUP_GEOLOCATIONSERVICEURL");
            _pingUrl = configuration.GetValue<string>("DOMAINLOOKUP_PINGSERVICEURL");
            _reverseDnsUrl = configuration.GetValue<string>("DOMAINLOOKUP_REVERSEDNSSERVICEURL");
        }

        [HttpPost]
        public IActionResult Post([FromBody]DomainInfoRequest request)
        {
            if (!_domainValidators.Any(x => x.IsValid(request.DomainName)))
                return BadRequest("Only valid IP addresses or domain names are accepted.");

            if (request.Services == null || request.Services.Count == 0)
                request.Services = SupportedServicesValidator.AllSupportedServices;
            else if (!SupportedServicesValidator.AreValid(request.Services))
                return BadRequest($"Only services from this list are supported: [{String.Join(", ", SupportedServicesValidator.AllSupportedServices)}]");

            Task<long> pingLookup = null;
            Task<string> reverseDnsLookup = null;
            Task<GeolocationDetails> geolocationLookup = null;

            if (request.Services.Contains("ping", StringComparer.CurrentCultureIgnoreCase))
                pingLookup = Task<long>.Factory.StartNew(() => { return QueueRequest<long>($"{_pingUrl}?domain={request.DomainName}"); });

            if (request.Services.Contains("geolocation", StringComparer.CurrentCultureIgnoreCase))
                geolocationLookup = Task<GeolocationDetails>.Factory.StartNew(() => { return QueueRequest<GeolocationDetails>($"{_geolocationUrl}?domain={request.DomainName}"); });

            if (request.Services.Contains("reverseDns", StringComparer.CurrentCultureIgnoreCase))
                reverseDnsLookup = Task<string>.Factory.StartNew(() => { return QueueRequest<string>($"{_reverseDnsUrl}?ipAddress={request.DomainName}"); });

            return Ok(new DomainInfoResponse
            {
                PingRoundtrip = pingLookup?.Result,
                Geolocation = geolocationLookup?.Result,
                Hostname = reverseDnsLookup?.Result
            });
        }

        private T QueueRequest<T>(string requestUrl, TimeSpan? maxWaitTime = null)
        {
            if (maxWaitTime == null)
                maxWaitTime = TimeSpan.FromSeconds(30);

            var returnTypeType = typeof(T);

            var request = _httpClient.GetStringAsync(requestUrl);
            request.Wait(maxWaitTime.Value);

            if (returnTypeType.IsPrimitive || returnTypeType == typeof(DateTime) || returnTypeType == typeof(string))
                return (T)Convert.ChangeType(request.Result, typeof(T));
            else
                return JsonConvert.DeserializeObject<T>(request.Result);
        }
    }
}
