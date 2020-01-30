using DomainLookupCore.SharedModels;
using NUnit.Framework;

namespace DomainLookupCoreTests.Core.SharedModels
{
    public class GeolocationDetailsTests
    {
        [Test]
        public void ParsingJsonReturnsExpectedResult()
        {
            var input = @"{
""ip"": ""127.0.0.1"",
""hostname"": ""localhost"",
""latitude"": ""7""
}";
            var output = GeolocationDetails.FromJson(input);

            Assert.AreEqual(output.Ip, "127.0.0.1");
            Assert.AreEqual(output.Hostname, "localhost");
            Assert.AreEqual(output.City, null);
            Assert.IsFalse(output.Longitude.HasValue);
            Assert.IsTrue(output.Latitude.HasValue);
        }
    }
}
