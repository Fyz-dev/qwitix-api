using qwitix_api.Core.Enums;
using qwitix_api.Core.Exceptions;
using qwitix_api.Core.Repositories;
using Stripe.Checkout;

namespace qwitix_api.Core.Services.StripeService
{
    public class StripeService(ITransactionRepository transactionRepository)
    {
        private readonly ITransactionRepository _transactionRepository = transactionRepository;

        public async void CheckoutSessionCompleted(Session session)
        {
            var transaction =
                await _transactionRepository.GetByCheckoutSessionId(session.Id)
                ?? throw new NotFoundException("Transaction not found.");

            transaction.StripePaymentIntentId = session.PaymentIntentId;
            transaction.Status = TransactionStatus.Completed;

            await _transactionRepository.UpdateById(transaction.Id, transaction);
        }

        public async void CheckoutSessionExpired(Session session)
        {
            var transaction =
                await _transactionRepository.GetByCheckoutSessionId(session.Id)
                ?? throw new NotFoundException("Transaction not found.");

            transaction.StripePaymentIntentId = session.PaymentIntentId;
            transaction.Status = TransactionStatus.Failed;

            await _transactionRepository.UpdateById(transaction.Id, transaction);
        }
    }
}
