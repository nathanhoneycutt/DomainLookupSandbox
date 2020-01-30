using System.Text.RegularExpressions;

namespace DomainLookupCore.Validation
{
    public class IpAddressValidator : IValidateDomains
    {
        public bool IsValid(string domain)
        {
            return Regex.IsMatch(domain, @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$");
        }
    }
}