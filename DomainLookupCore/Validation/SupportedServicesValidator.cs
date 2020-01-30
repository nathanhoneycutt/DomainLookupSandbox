using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainLookupCore.Validation
{
	public static class SupportedServicesValidator
    {
        public static List<string> AllSupportedServices { get; } = new List<string> { "ping", "reverseDns", "geolocation" };

		public static bool AreValid(IEnumerable<string> services)
		{
			return !services.Any(x => !AllSupportedServices.Contains(x, StringComparer.CurrentCultureIgnoreCase));
		}
	}
}
