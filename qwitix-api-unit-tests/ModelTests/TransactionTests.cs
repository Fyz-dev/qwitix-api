using qwitix_api.Core.Enums;
using qwitix_api.Core.Exceptions;
using qwitix_api.Core.Models;

namespace qwitix_api_unit_tests
{
    [TestClass]
    public class TransactionTests
    {
        [TestMethod]
        [DataRow("507f1f77bcf86cd799439011")]
        public void SetUserId_Valid_SetsValue(string userId)
        {
            var transaction = new Transaction { UserId = userId };
            Assert.AreEqual(userId, transaction.UserId);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("   ")]
        [ExpectedException(typeof(ValidationException))]
        public void SetUserId_Invalid_ThrowsValidationException(string userId)
        {
            var transaction = new Transaction { UserId = userId };
        }

        [TestMethod]
        [DataRow("USD")]
        [DataRow("EUR")]
        public void SetCurrency_Valid_SetsValue(string currency)
        {
            var transaction = new Transaction { Currency = currency };
            Assert.AreEqual(currency, transaction.Currency);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("   ")]
        [ExpectedException(typeof(ValidationException))]
        public void SetCurrency_Invalid_ThrowsValidationException(string currency)
        {
            var transaction = new Transaction { Currency = currency };
        }

        [TestMethod]
        [DataRow("session_12345")]
        public void SetStripeCheckoutSession_Valid_SetsValue(string session)
        {
            var transaction = new Transaction { StripeCheckoutSession = session };
            Assert.AreEqual(session, transaction.StripeCheckoutSession);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("   ")]
        [ExpectedException(typeof(ValidationException))]
        public void SetStripeCheckoutSession_Invalid_ThrowsValidationException(string session)
        {
            var transaction = new Transaction { StripeCheckoutSession = session };
        }

        [TestMethod]
        [DataRow(TransactionStatus.Completed)]
        [DataRow(TransactionStatus.Pending)]
        public void SetStatus_Valid_SetsValue(TransactionStatus status)
        {
            var transaction = new Transaction { Status = status };
            Assert.AreEqual(status, transaction.Status);
        }

        [TestMethod]
        [DataRow("1", 1)]
        [DataRow("2", 2)]
        public void SetTickets_Valid_SetsValue(string ticketId, int quantity)
        {
            var tickets = new List<TicketPurchase>
            {
                new TicketPurchase { TicketId = ticketId, Quantity = quantity },
            };

            var transaction = new Transaction { Tickets = tickets };
            Assert.AreEqual(1, transaction.Tickets.Count);
            Assert.AreEqual(ticketId, transaction.Tickets[0].TicketId);
            Assert.AreEqual(quantity, transaction.Tickets[0].Quantity);
        }

        [TestMethod]
        [DataRow("pi_12345")]
        [DataRow(null)]
        public void SetStripePaymentIntentId_Optional_SetsValue(string? paymentIntentId)
        {
            var transaction = new Transaction { StripePaymentIntentId = paymentIntentId };
            Assert.AreEqual(paymentIntentId, transaction.StripePaymentIntentId);
        }
    }
}
