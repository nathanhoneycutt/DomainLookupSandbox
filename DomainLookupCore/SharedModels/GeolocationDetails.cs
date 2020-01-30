using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLookupCore.SharedModels
{
    public class GeolocationDetails
    {
        public string Ip { get; set; }
        public string Hostname { get; set; }
        public string ContinentCode { get; set; }
        public string ContinentName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Isp { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public static GeolocationDetails FromJson(string jsonString)
        {
            return JsonConvert.DeserializeObject<GeolocationDetails>(jsonString);
        }
    }
}
