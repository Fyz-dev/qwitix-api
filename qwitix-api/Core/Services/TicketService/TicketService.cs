using System.Diagnostics;
using qwitix_api.Core.Exceptions;
using qwitix_api.Core.Helpers;
using qwitix_api.Core.Mappers;
using qwitix_api.Core.Models;
using qwitix_api.Core.Repositories;
using qwitix_api.Core.Services.TicketService.DTOs;
using qwitix_api.Infrastructure.Integration.StripeIntegration;

namespace qwitix_api.Core.Services.TicketService
{
    public class TicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly StripeIntegration _stripeIntegration;
        private readonly IMapper<CreateTicketDTO, Ticket> _createTicketMapper;
        private readonly IMapper<ResponseTicketDTO, Ticket> _responseTicketMapper;

        public TicketService(
            ITicketRepository ticketRepository,
            StripeIntegration stripeIntegration,
            IMapper<CreateTicketDTO, Ticket> createTicketMapper,
            IMapper<ResponseTicketDTO, Ticket> responseTicketMapper
        )
        {
            _ticketRepository = ticketRepository;
            _stripeIntegration = stripeIntegration;
            _createTicketMapper = createTicketMapper;
            _responseTicketMapper = responseTicketMapper;
        }

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
            var price = await _stripeIntegration.CreatePriceAsync(ticket.Price, "usd", product.Id);

            ticket.StripePriceId = price.Id;

            await _ticketRepository.UpdateById(ticket.Id, ticket);
        }

        public async Task<ResponseBuyTicketDTO> BuyById(string id, BuyTicketDTO buyTicketDTO)
        {
            var ticket =
                await _ticketRepository.GetById(id)
                ?? throw new NotFoundException("Ticket not found.");

            var session = await _stripeIntegration.CreateCheckoutSessionAsync(
                ticket.StripePriceId,
                buyTicketDTO.SuccessUrl,
                buyTicketDTO.CancelUrl,
                buyTicketDTO.Quantity,
                "cus_S50OzLXnY0Yjj9"
            );

            return new ResponseBuyTicketDTO { url = session.Url };
        }

        public async Task<IEnumerable<ResponseTicketDTO>> GetAll(
            string eventId,
            int offset,
            int limit
        )
        {
            var tickets = await _ticketRepository.GetAll(eventId, offset, limit);

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

            PatchHelper.ApplyPatch(ticketDTO, ticket);

            await _stripeIntegration.UpdateProductAsync(ticket.Id, ticket.Name, ticket.Details);

            await _ticketRepository.UpdateById(id, ticket);
        }

        public async Task DeleteById(string id)
        {
            var ticket =
                await _ticketRepository.GetById(id)
                ?? throw new NotFoundException($"Ticket not found.");

            await _ticketRepository.DeleteById(id);
            //await _stripeIntegration.DeleteProductAsync(ticket.Id);
        }
    }
}
