using DomainLookupCore.Validation;
using NUnit.Framework;

namespace DomainLookupCoreTests.Core.Validation
{
    public class SupportedServicesValidatorTests
    {
        [Test]
        public void FullValidListIsAccepted()
        {
            Assert.IsTrue(
                SupportedServicesValidator.AreValid(new[] { "ping", "reverseDns", "geolocation" })
            );
        }

        [Test]
        public void SingleValidServiceIsAccepted()
        {
            Assert.IsTrue(
                SupportedServicesValidator.AreValid(new[] { "ping" })
            );
        }

        [Test]
        public void SingleInvalidServiceIsRejected()
        {
            Assert.IsFalse(
                SupportedServicesValidator.AreValid(new[] { "magic" })
            );
        }

        [Test]
        public void ListWithBothValidAndInvalidIsRejected()
        {
            Assert.IsFalse(
                SupportedServicesValidator.AreValid(new[] { "ping", "reverseDns", "magic" })
            );
        }
    }
}
