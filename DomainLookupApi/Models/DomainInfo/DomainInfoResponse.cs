using DomainLookupCore.SharedModels;

namespace DomainLookupApi.Models.DomainInfo
{
    public class DomainInfoResponse
    {
        public GeolocationDetails Geolocation { get; set; }
        public string Hostname { get; set; }
        public long? PingRoundtrip { get; set; }
    }
}