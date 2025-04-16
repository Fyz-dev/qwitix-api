using qwitix_api.Core.Exceptions;
using qwitix_api.Core.Models;

namespace qwitix_api_unit_tests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        [DataRow("John Doe")]
        public void SetFullName_Valid_SetsValue(string fullName)
        {
            var user = new User { FullName = fullName };
            Assert.AreEqual(fullName, user.FullName);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("   ")]
        [ExpectedException(typeof(ValidationException))]
        public void SetFullName_Invalid_ThrowsValidationException(string fullName)
        {
            var user = new User { FullName = fullName };
        }

        [TestMethod]
        [DataRow("test@example.com")]
        [DataRow("user@domain.co")]
        public void SetEmail_Valid_SetsValue(string email)
        {
            var user = new User { Email = email };
            Assert.AreEqual(email, user.Email);
        }

        [TestMethod]
        [DataRow("invalid-email")]
        [DataRow("user@domain")]
        [DataRow("user@.com")]
        [ExpectedException(typeof(ValidationException))]
        public void SetEmail_Invalid_ThrowsValidationException(string email)
        {
            var user = new User { Email = email };
        }

        [TestMethod]
        [DataRow("cus_1234567890")]
        [DataRow("cus_abcd1234")]
        public void SetStripeCustomerId_Valid_SetsValue(string stripeCustomerId)
        {
            var user = new User { StripeCustomerId = stripeCustomerId };
            Assert.AreEqual(stripeCustomerId, user.StripeCustomerId);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("   ")]
        [ExpectedException(typeof(ValidationException))]
        public void SetStripeCustomerId_Invalid_ThrowsValidationException(string stripeCustomerId)
        {
            var user = new User { StripeCustomerId = stripeCustomerId };
        }
    }
}
