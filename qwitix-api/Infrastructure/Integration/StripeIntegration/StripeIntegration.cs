using Microsoft.Extensions.Options;
using qwitix_api.Core.Models;
using qwitix_api.Infrastructure.Configs;
using Stripe;
using Stripe.Checkout;

namespace qwitix_api.Infrastructure.Integration.StripeIntegration
{
    public class StripeIntegration
    {
        public StripeIntegration(IOptions<StripeSettings> stripeSettings)
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
            string productId,
            string name,
            string? description,
            decimal price,
            string currency
        )
        {
            var productOptions = new ProductCreateOptions { Id = productId, Name = name };

            if (!string.IsNullOrWhiteSpace(description))
                productOptions.Description = description;

            var productService = new ProductService();
            var product = await productService.CreateAsync(productOptions);

            return product;
        }

        public async Task<Price> CreatePriceAsync(decimal price, string currency, string productId)
        {
            var priceOptions = new PriceCreateOptions
            {
                UnitAmountDecimal = (decimal)price * 100,
                Currency = currency,
                Product = productId,
            };

            var priceService = new PriceService();

            return await priceService.CreateAsync(priceOptions);
        }

        public async Task<Product> UpdateProductAsync(
            string productId,
            string? name,
            string? description
        )
        {
            var updateOptions = new ProductUpdateOptions { Name = name, Description = description };

            var productService = new ProductService();

            return await productService.UpdateAsync(productId, updateOptions);
        }

        public async Task DeleteProductAsync(string productId)
        {
            var service = new ProductService();

            await service.DeleteAsync(productId);
        }

        public async Task<Session> CreateCheckoutSessionAsync(
            List<(string PriceId, int Quantity)> tickets,
            string successUrl,
            string cancelUrl,
            string customerId
        )
        {
            var lineItems = tickets
                .Select(ticket => new SessionLineItemOptions
                {
                    Price = ticket.PriceId,
                    Quantity = ticket.Quantity,
                })
                .ToList();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
                Customer = customerId,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
            };

            var service = new SessionService();

            return await service.CreateAsync(options);
        }

        public async Task<Refund> CreateRefundAsync(string paymentIntent)
        {
            var refundOptions = new RefundCreateOptions { PaymentIntent = paymentIntent };

            var refundService = new RefundService();

            return await refundService.CreateAsync(refundOptions);
        }
    }
}
