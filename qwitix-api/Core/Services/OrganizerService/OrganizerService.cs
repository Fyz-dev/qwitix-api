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
            throw new NotImplementedException();
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
