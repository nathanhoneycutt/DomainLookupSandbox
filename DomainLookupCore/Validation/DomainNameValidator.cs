using System;
using System.Text;
using System.Text.RegularExpressions;

namespace DomainLookupCore.Validation
{
    public class DomainNameValidator : IValidateDomains
    {
        public bool IsValid(string domain)
        {
            return Regex.IsMatch(domain, @"\.(edu|com|net|org)$");
        }
    }
}