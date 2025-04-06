using Microsoft.Extensions.Options;
using qwitix_api.Infrastructure.Configs;
using Stripe;
using Stripe.Checkout;

namespace qwitix_api.Infrastructure.Service.StripeService
{
    public class StripeService
    {
        public StripeService(IOptions<StripeSettings> stripeSettings)
        {
            StripeConfiguration.ApiKey = stripeSettings.Value.SecretKey;
        }

        public async Task<Customer> CreateCustomerAsync(string name, string email)
        {
            var options = new CustomerCreateOptions { Email = email, Name = name };

            var service = new CustomerService();

            return await service.CreateAsync(options);
        }

        public async Task<Product> CreateProductAsync(
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

            var product = await productService.CreateAsync(productOptions);

            await CreatePriceAsync(unitAmount, currency, product.Id);

            return product;
        }

        public async Task DeleteProductAsync(string productId)
        {
            var service = new ProductService();

            await service.DeleteAsync(productId);
        }

        public async Task<Session> CreateCheckoutSessionAsync(
            string priceId,
            string successUrl,
            string cancelUrl
        )
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

            return await service.CreateAsync(options);
        }

        public async Task<Refund> CreateRefundAsync(string chargeId)
        {
            var refundOptions = new RefundCreateOptions { Charge = chargeId };

            var refundService = new RefundService();

            return await refundService.CreateAsync(refundOptions);
        }

        private async Task CreatePriceAsync(long unitAmount, string currency, string productId)
        {
            var priceOptions = new PriceCreateOptions
            {
                UnitAmount = unitAmount,
                Currency = currency,
                Product = productId,
            };

            var priceService = new PriceService();
            await priceService.CreateAsync(priceOptions);
        }
    }
}
