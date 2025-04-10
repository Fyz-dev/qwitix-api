using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using qwitix_api.Core.Dispatcher;
using qwitix_api.Infrastructure.Configs;
using Stripe;

namespace qwitix_api.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/stripe")]
    public class StripeController(
        IOptions<StripeSettings> stripeSettings,
        ILogger<StripeController> logger,
        StripeEventDispatcher eventDispatcher
    ) : ControllerBase
    {
        private readonly string _endpointSecret = stripeSettings.Value.WebhookSecret;
        private readonly ILogger<StripeController> _logger = logger;
        private readonly StripeEventDispatcher _eventDispatcher = eventDispatcher;

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

                _eventDispatcher.RaiseStripeEvent(stripeEvent);

                return Ok();
            }
            catch (StripeException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //private void HandleStripeEvent(Event stripeEvent)
        //{
        //    switch (stripeEvent.Type)
        //    {
        //        case EventTypes.ChargeSucceeded:
        //            var chargeSucceeded = stripeEvent.Data.Object as Charge;

        //            _logger.LogInformation("Charge succeeded: {ChargeId}", chargeSucceeded?.Id);

        //            break;

        //        case EventTypes.ChargeRefunded:
        //            var chargeRefunded = stripeEvent.Data.Object as Charge;

        //            _logger.LogInformation("Charge succeeded: {ChargeId}", chargeRefunded?.Id);

        //            break;

        //        case EventTypes.ChargeExpired:
        //            var chargeFailed = stripeEvent.Data.Object as Charge;

        //            _logger.LogWarning(
        //                "Charge failed: {ChargeId}, Reason: {Reason}",
        //                chargeFailed?.Id,
        //                chargeFailed?.FailureMessage
        //            );
        //            break;

        //        default:

        //            _logger.LogInformation("Unhandled event type: {EventType}", stripeEvent.Type);
        //            break;
        //    }
        //}
    }
}
