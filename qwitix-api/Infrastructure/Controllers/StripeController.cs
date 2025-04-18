using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using qwitix_api.Core.Dispatcher;
using qwitix_api.Core.Services.StripeService;
using qwitix_api.Infrastructure.Configs;
using Stripe;
using Stripe.Checkout;

namespace qwitix_api.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/stripe")]
    public class StripeController(
        StripeService stripeService,
        IOptions<StripeSettings> stripeSettings,
        ILogger<StripeController> logger
    ) : ControllerBase
    {
        private readonly StripeService _stripeService = stripeService;
        private readonly string _endpointSecret = stripeSettings.Value.WebhookSecret;

        private readonly ILogger<StripeController> _logger = logger;

        [HttpPost("webhooks")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var signatureHeader = Request.Headers["Stripe-Signature"];

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    signatureHeader,
                    _endpointSecret
                );

                _logger.LogInformation("Stripe event received: {EventType}", stripeEvent.Type);

                HandleStripeEvent(stripeEvent);

                return Ok();
            }
            catch (StripeException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void HandleStripeEvent(Event stripeEvent)
        {
            switch (stripeEvent.Type)
            {
                case EventTypes.CheckoutSessionCompleted:
                {
                    var session =
                        stripeEvent.Data.Object as Session
                        ?? throw new InvalidOperationException("Invalid session object.");

                    _stripeService.CheckoutSessionCompleted(session);
                    break;
                }

                case EventTypes.CheckoutSessionExpired:
                {
                    var session =
                        stripeEvent.Data.Object as Session
                        ?? throw new InvalidOperationException("Invalid session object.");

                    _stripeService.CheckoutSessionExpired(session);
                    break;
                }

                default:
                {
                    _logger.LogInformation("Unhandled event type: {EventType}", stripeEvent.Type);
                    break;
                }
            }
        }
    }
}
