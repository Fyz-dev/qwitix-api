using qwitix_api.Core.Repositories;
using qwitix_api.Core.Services.OrganizerService.DTOs;

namespace qwitix_api.Core.Services.OrganizerService
{
    public class OrganizerService
    {
        private readonly IOrganizerRepository _organizerRepository;

        public OrganizerService(IOrganizerRepository organizerRepository)
        {
            _organizerRepository = organizerRepository;
        }

        public async Task Create(CreateOrganizerDTO organizerDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ResponseOrganizerDTO>> GetAll(int offset, int limit)
        {
            var organizers = await _organizerRepository.GetAll(offset, limit);

            return organizers.Select(o => new ResponseOrganizerDTO
            {
                Id = o.Id,
                UserId = o.UserId,
                Name = o.Name,
                Bio = o.Bio,
                ImageUrl = o.ImageUrl,
                IsVerified = o.IsVerified,
                UpdatedAt = o.UpdatedAt,
                CreatedAt = o.CreatedAt,
            });
        }

        public async Task<ResponseOrganizerDTO> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateById(string id, UpdateOrganizerDTO organizerDTO)
        {
            throw new NotImplementedException();
        }
    }
}
