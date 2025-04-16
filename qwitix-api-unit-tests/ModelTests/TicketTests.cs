using qwitix_api.Core.Exceptions;
using qwitix_api.Core.Models;

namespace qwitix_api_unit_tests
{
    [TestClass]
    public class TicketTests
    {
        public static IEnumerable<object[]> InvalidNameData =>
            new List<object[]>
            {
                new object[] { "" },
                new object[] { "   " },
                new object[] { new string('A', 101) },
            };

        public static IEnumerable<object[]> ValidNameData =>
            new List<object[]>
            {
                new object[] { "Regular Ticket" },
                new object[] { new string('B', 100) },
            };

        public static IEnumerable<object[]> InvalidDetailsData =>
            new List<object[]> { new object[] { new string('D', 801) } };

        public static IEnumerable<object[]> ValidDetailsData =>
            new List<object[]>
            {
                new object[] { "" },
                new object[] { "This is a valid description of the ticket." },
                new object[] { new string('C', 800) },
            };

        public static IEnumerable<object[]> ValidPriceData =>
            new List<object[]>
            {
                new object[] { 0m },
                new object[] { 0.01m },
                new object[] { 100.5m },
            };

        public static IEnumerable<object[]> InvalidPriceData =>
            new List<object[]> { new object[] { -1m }, new object[] { -0.01m } };

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        [ExpectedException(typeof(ValidationException))]
        public void SetEventId_Invalid_ThrowsValidationException(string eventId)
        {
            var ticket = new Ticket { EventId = eventId };
        }

        [TestMethod]
        [DataRow("507f1f77bcf86cd799439011")]
        public void SetEventId_Valid_SetsValue(string eventId)
        {
            var ticket = new Ticket { EventId = eventId };
            Assert.AreEqual(eventId, ticket.EventId);
        }

        [TestMethod]
        [DynamicData(nameof(InvalidNameData))]
        [ExpectedException(typeof(ValidationException))]
        public void SetName_Invalid_ThrowsValidationException(string name)
        {
            var ticket = new Ticket { Name = name };
        }

        [TestMethod]
        [DynamicData(nameof(ValidNameData))]
        public void SetName_Valid_SetsValue(string name)
        {
            var ticket = new Ticket { Name = name };
            Assert.AreEqual(name, ticket.Name);
        }

        [TestMethod]
        [DynamicData(nameof(ValidDetailsData))]
        public void SetDetails_Valid_SetsValue(string details)
        {
            var ticket = new Ticket { Details = details };
            Assert.AreEqual(details, ticket.Details);
        }

        [TestMethod]
        [DynamicData(nameof(InvalidDetailsData))]
        [ExpectedException(typeof(ValidationException))]
        public void SetDetails_TooLong_ThrowsValidationException(string details)
        {
            var ticket = new Ticket { Details = details };
        }

        [TestMethod]
        [DynamicData(nameof(ValidPriceData))]
        public void SetPrice_Valid_SetsValue(decimal price)
        {
            var ticket = new Ticket { Price = price };
            Assert.AreEqual(price, ticket.Price);
        }

        [TestMethod]
        [DynamicData(nameof(InvalidPriceData))]
        [ExpectedException(typeof(ValidationException))]
        public void SetPrice_Negative_ThrowsValidationException(decimal price)
        {
            var ticket = new Ticket { Price = price };
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(1000)]
        public void SetQuantity_Valid_SetsValue(int quantity)
        {
            var ticket = new Ticket { Quantity = quantity };
            Assert.AreEqual(quantity, ticket.Quantity);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(-100)]
        [ExpectedException(typeof(ValidationException))]
        public void SetQuantity_Negative_ThrowsValidationException(int quantity)
        {
            var ticket = new Ticket { Quantity = quantity };
        }

        [TestMethod]
        public void SetStripePriceId_AnyValue_SetsValue()
        {
            var ticket = new Ticket { StripePriceId = "price_12345" };
            Assert.AreEqual("price_12345", ticket.StripePriceId);
        }

        [TestMethod]
        public void SetStripePriceId_Null_DoesNotThrow()
        {
            var ticket = new Ticket { StripePriceId = null };
            Assert.IsNull(ticket.StripePriceId);
        }

        [TestMethod]
        public void Ticket_ValidFullObject_CreatesSuccessfully()
        {
            var ticket = new Ticket
            {
                EventId = "507f1f77bcf86cd799439011",
                Name = "VIP Pass",
                Price = 99.99m,
                Quantity = 50,
                StripePriceId = "price_abc123",
                Details = "Access to VIP lounge and early entry.",
            };

            Assert.AreEqual("507f1f77bcf86cd799439011", ticket.EventId);
            Assert.AreEqual("VIP Pass", ticket.Name);
            Assert.AreEqual(99.99m, ticket.Price);
            Assert.AreEqual(50, ticket.Quantity);
            Assert.AreEqual("price_abc123", ticket.StripePriceId);
            Assert.AreEqual("Access to VIP lounge and early entry.", ticket.Details);
        }
    }
}
