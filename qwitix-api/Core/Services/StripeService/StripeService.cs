using Microsoft.Extensions.Options;
using qwitix_api.Infrastructure.Configs;
using Stripe;
using Stripe.Checkout;

namespace qwitix_api.Core.Services.StripeService
{
    public class StripeService
    {
        public StripeService(IOptions<StripeSettings> stripeSettings)
        {
            StripeConfiguration.ApiKey = stripeSettings.Value.SecretKey;
        }

        public Customer CreateCustomer(string email, string name)
        {
            var options = new CustomerCreateOptions { Email = email, Name = name };

            var service = new CustomerService();

            return service.Create(options);
        }

        public Product CreateProduct(
            string name,
            string description,
            long unitAmount,
            string currency
        )
        {
            var productOptions = new ProductCreateOptions
            {
                Name = name,
                Description = description,
            };

            var productService = new ProductService();
            var product = productService.Create(productOptions);

            var priceOptions = new PriceCreateOptions
            {
                UnitAmount = unitAmount,
                Currency = currency,
                Product = product.Id,
            };

            var priceService = new PriceService();
            priceService.Create(priceOptions);

            return product;
        }

        public void DeleteProduct(string productId)
        {
            var service = new ProductService();

            service.Delete(productId);
        }

        public Session CreateCheckoutSession(string priceId, string successUrl, string cancelUrl)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions { Price = priceId, Quantity = 1 },
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return session;
        }

        public Refund CreateRefund(string chargeId)
        {
            var refundOptions = new RefundCreateOptions { Charge = chargeId };

            var refundService = new RefundService();
            Refund refund = refundService.Create(refundOptions);

            return refund;
        }
    }
}
