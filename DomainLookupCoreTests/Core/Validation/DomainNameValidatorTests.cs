using DomainLookupCore.Validation;
using NUnit.Framework;

namespace DomainLookupCoreTests.Core.Validation
{
    public class DomainNameValidatorTests
    {
        private DomainNameValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new DomainNameValidator();
        }

        [Test]
        public void DomainNamesPass()
        {
            Assert.IsTrue(_validator.IsValid("www.google.com"));
            Assert.IsTrue(_validator.IsValid("800flowers.com"));
        }

        [Test]
        public void IpAddressesFail()
        {
            Assert.IsFalse(_validator.IsValid("127.0.0.1"));
        }
    }
}
