﻿using qwitix_api.Core.Enums;
using qwitix_api.Core.Models;

namespace qwitix_api.Core.Repositories.EventRepository
{
    public interface IEventRepository
    {
        Task Create(Event eventModel);

        Task<(IEnumerable<Event> Items, int TotalCount)> GetAll(
            string? organizerId,
            int offset,
            int limit,
            List<EventStatus>? statuses = null,
            string? searchQuery = null,
            List<string>? categories = null
        );

        Task<Event?> GetById(string id);

        Task<IEnumerable<Event>> GetById(params string[] ids);

        Task UpdateById(string id, Event eventModel);

        Task DeleteById(string id);

        Task<IEnumerable<string>> GetUniqueCategories();
    }
}
