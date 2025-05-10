using qwitix_api.Core.Dispatcher;
using qwitix_api.Core.Enums;
using qwitix_api.Core.Exceptions;
using qwitix_api.Core.Helpers;
using qwitix_api.Core.Mappers;
using qwitix_api.Core.Models;
using qwitix_api.Core.Repositories;
using qwitix_api.Core.Repositories.EventRepository;
using qwitix_api.Core.Services.EventService.DTOs;
using qwitix_api.Core.Services.TicketService.DTOs;
using qwitix_api.Core.Services.TransactionService.DTOs;
using qwitix_api.Infrastructure.Repositories;

namespace qwitix_api.Core.Services.TransactionService
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactonRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper<ResponseTicketDTO, Ticket> _responseTicketMapper;

        public TransactionService(
            ITransactionRepository transactonRepository,
            IUserRepository userRepository,
            ITicketRepository ticketRepository,
            IEventRepository eventRepository,
            IMapper<ResponseTicketDTO, Ticket> responseTicketMapper,
            IMapper<ResponseEventDTO, Event> responseEventMapper
        )
        {
            _transactonRepository = transactonRepository;
            _ticketRepository = ticketRepository;
            _responseTicketMapper = responseTicketMapper;
        }

        public async Task<PaginationResponse<ResponseTransactionDTO>> GetByUserId(
            string userId,
            int offset,
            int limit,
            TransactionStatus? status = null
        )
        {
            var (transactions, totalCount) = await _transactonRepository.GetByUserId(
                userId,
                offset,
                limit,
                status
            );

            var allTicketIds = transactions
                .SelectMany(t => t.Tickets)
                .Select(t => t.TicketId)
                .Distinct()
                .ToArray();

            var tickets = (await _ticketRepository.GetById(allTicketIds)).ToDictionary(t => t.Id);

            var transactionDTOs = transactions
                .Select(transaction =>
                {
                    var ticketDTOs = transaction
                        .Tickets.Select(t =>
                        {
                            if (!tickets.TryGetValue(t.TicketId, out var ticket))
                                throw new NotFoundException(
                                    $"Ticket with ID {t.TicketId} not found."
                                );

                            return _responseTicketMapper.ToDto(ticket);
                        })
                        .ToList();

                    return new ResponseTransactionDTO
                    {
                        Id = transaction.Id,
                        CreatedAt = transaction.CreatedAt,
                        UpdatedAt = transaction.UpdatedAt,
                        UserId = transaction.UserId,
                        Currency = transaction.Currency,
                        Status = transaction.Status,
                        Tickets = ticketDTOs,
                    };
                })
                .ToList();

            bool hasNextPage = (limit > 0) && (offset + limit < totalCount);

            return new PaginationResponse<ResponseTransactionDTO>
            {
                Items = transactionDTOs,
                TotalCount = totalCount,
                HasNextPage = hasNextPage,
            };
        }

        public async Task<ResponseTransactionDTO> GetByTransactionId(string id)
        {
            var transaction =
                await _transactonRepository.GetByTransactionId(id)
                ?? throw new NotFoundException("Transaction not found.");

            var ticketIds = transaction.Tickets.Select(t => t.TicketId).ToArray();
            var tickets = (await _ticketRepository.GetById(ticketIds)).ToDictionary(t => t.Id);

            var ticketDtos = transaction
                .Tickets.Select(t =>
                {
                    if (!tickets.TryGetValue(t.TicketId, out var ticket))
                        throw new NotFoundException($"Ticket with ID {t.TicketId} not found.");

                    return _responseTicketMapper.ToDto(ticket);
                })
                .ToList();

            return new ResponseTransactionDTO
            {
                Id = transaction.Id,
                CreatedAt = transaction.CreatedAt,
                UpdatedAt = transaction.UpdatedAt,
                UserId = transaction.UserId,
                Currency = transaction.Currency,
                Status = transaction.Status,
                Tickets = ticketDtos,
            };
        }
    }
}
