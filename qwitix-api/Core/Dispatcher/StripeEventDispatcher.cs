using Stripe;

namespace qwitix_api.Core.Dispatcher
{
    public class StripeEventDispatcher
    {
        private readonly Dictionary<string, List<Action<Event>>> _handlersByType = new();

        public void RegisterHandler(string eventType, Action<Event> handler)
        {
            if (!_handlersByType.TryGetValue(eventType, out var handlers))
            {
                handlers = new List<Action<Event>>();
                _handlersByType[eventType] = handlers;
            }

            handlers.Add(handler);
        }

        public void RaiseStripeEvent(Event stripeEvent)
        {
            if (_handlersByType.TryGetValue(stripeEvent.Type, out var handlers))
                foreach (var handler in handlers)
                    handler.Invoke(stripeEvent);
        }
    }
}
