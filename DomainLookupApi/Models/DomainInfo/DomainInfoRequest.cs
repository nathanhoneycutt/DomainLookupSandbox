using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainLookupApi.Models.DomainInfo
{
    public class DomainInfoRequest
    {
        public string DomainName { get; set; }
        public List<string> Services { get; set; }
    }
}
