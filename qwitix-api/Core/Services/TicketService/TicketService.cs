using qwitix_api.Core.Enums;
using qwitix_api.Core.Exceptions;
using qwitix_api.Core.Helpers;
using qwitix_api.Core.Mappers;
using qwitix_api.Core.Models;
using qwitix_api.Core.Repositories;
using qwitix_api.Core.Repositories.EventRepository;
using qwitix_api.Core.Services.TicketService.DTOs;
using qwitix_api.Infrastructure.Integration.StripeIntegration;

namespace qwitix_api.Core.Services.TicketService
{
    public class TicketService(
        ITicketRepository ticketRepository,
        ITransactionRepository transactionRepository,
        IEventRepository eventRepository,
        IUserRepository userRepository,
        StripeIntegration stripeIntegration,
        IMapper<CreateTicketDTO, Ticket> createTicketMapper,
        IMapper<ResponseTicketDTO, Ticket> responseTicketMapper,
        IMapper<TicketPurchaseDTO, TicketPurchase> ticketPurchaseMapper
    )
    {
        private readonly ITicketRepository _ticketRepository = ticketRepository;
        private readonly ITransactionRepository _transactionRepository = transactionRepository;
        private readonly IEventRepository _eventRepository = eventRepository;
        private readonly IUserRepository _userRepository = userRepository;

        private readonly StripeIntegration _stripeIntegration = stripeIntegration;
        private readonly IMapper<CreateTicketDTO, Ticket> _createTicketMapper = createTicketMapper;
        private readonly IMapper<ResponseTicketDTO, Ticket> _responseTicketMapper =
            responseTicketMapper;
        private readonly IMapper<TicketPurchaseDTO, TicketPurchase> _ticketPurchaseMapper =
            ticketPurchaseMapper;

        public async Task Create(CreateTicketDTO ticketDTO)
        {
            var ticket = await _ticketRepository.Create(_createTicketMapper.ToEntity(ticketDTO));

            var product = await _stripeIntegration.CreateProductAsync(
                ticket.Id,
                ticket.Name,
                ticket.Details,
                ticket.Price,
                "usd"
            );
        }

        public async Task<ResponseBuyTicketDTO> BuyById(string userId, BuyTicketDTO buyTicketDTO)
        {
            var ticketIds = buyTicketDTO
                .Tickets.Select(ticketPurchase => ticketPurchase.TicketId)
                .ToArray();
            var tickets = await _ticketRepository.GetById(ticketIds);
            var totalSoldQuantity = await _transactionRepository.GetTotalSoldQuantityForTickets(
                ticketIds
            );
            var quantityMap = buyTicketDTO.Tickets.ToDictionary(t => t.TicketId, t => t.Quantity);

            foreach (var ticket in tickets)
            {
                var alreadySold = totalSoldQuantity.GetValueOrDefault(ticket.Id);
                var availableQuantity = ticket.Quantity - alreadySold;

                if (
                    quantityMap.TryGetValue(ticket.Id, out var requestedQuantity)
                    && requestedQuantity > availableQuantity
                )
                    throw new ValidationException(
                        $"Not enough tickets available for {ticket.Name}."
                    );
            }

            var user =
                await _userRepository.GetById(userId)
                ?? throw new NotFoundException("User not found.");

            var session = await _stripeIntegration.CreateCheckoutSessionAsync(
                [
                    .. tickets
                        .Where(ticket => quantityMap.ContainsKey(ticket.Id))
                        .Select(ticket => (ticket.StripePriceId, quantityMap[ticket.Id])),
                ],
                buyTicketDTO.SuccessUrl,
                buyTicketDTO.CancelUrl,
                user.StripeCustomerId
            );

            var tr = new Transaction
            {
                UserId = user.Id,
                Tickets = _ticketPurchaseMapper.ToEntityList(buyTicketDTO.Tickets),
                Currency = session.Currency,
                Status = TransactionStatus.Pending,
                StripeCheckoutSession = session.Id,
                StripePaymentLink = session.Url,
            };

            await _transactionRepository.Create(tr);

            return new ResponseBuyTicketDTO { url = session.Url };
        }

        public async Task<IEnumerable<ResponseTicketDTO>> GetAll(string eventId)
        {
            var tickets = await _ticketRepository.GetAll(eventId);

            return _responseTicketMapper.ToDtoList(tickets);
        }

        public async Task<ResponseTicketDTO> GetById(string id)
        {
            var ticket =
                await _ticketRepository.GetById(id)
                ?? throw new NotFoundException($"Ticket not found.");

            return _responseTicketMapper.ToDto(ticket);
        }

        public async Task UpdateById(string id, UpdateTicketDTO ticketDTO)
        {
            var ticket =
                await _ticketRepository.GetById(id)
                ?? throw new NotFoundException($"Ticket not found.");

            if (ticket.StripePriceId is not null)
                throw new ValidationException(
                    "You cannot update a ticket for an event that is already been published, cancelled or rescheduled."
                );

            PatchHelper.ApplyPatch(ticketDTO, ticket);

            await _stripeIntegration.UpdateProductAsync(ticket.Id, ticket.Name, ticket.Details);

            await _ticketRepository.UpdateById(id, ticket);
        }

        public async Task DeleteById(string id)
        {
            var ticket =
                await _ticketRepository.GetById(id)
                ?? throw new NotFoundException($"Ticket not found.");

            var eventModel =
                await _eventRepository.GetById(ticket.EventId)
                ?? throw new NotFoundException($"Event not found.");

            if (eventModel.Status != EventStatus.Draft)
                throw new ValidationException(
                    "You cannot delete a ticket for an event that is already been published, cancelled or rescheduled."
                );

            await _ticketRepository.DeleteById(id);
            await _stripeIntegration.DeleteProductAsync(ticket.Id);
        }
    }
}
