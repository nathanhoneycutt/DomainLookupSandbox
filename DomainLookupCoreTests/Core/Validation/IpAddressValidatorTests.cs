using NUnit.Framework;
using DomainLookupCore.Validation;

namespace DomainLookupCoreTests.Core.Validation
{
    public class IpAddressValidatorTests
    {
        private IpAddressValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new IpAddressValidator();
        }

        [Test]
        public void ValidAddressesPass()
        {
            Assert.IsTrue(_validator.IsValid("127.0.0.1"));
        }

        [Test]
        public void DomainNamesFail()
        {
            Assert.IsFalse(_validator.IsValid("www.google.com"));
            Assert.IsFalse(_validator.IsValid("800flowers.com"));
        }
    }
}
