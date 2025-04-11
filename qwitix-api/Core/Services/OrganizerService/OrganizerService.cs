using qwitix_api.Core.Exceptions;
using qwitix_api.Core.Helpers;
using qwitix_api.Core.Mappers;
using qwitix_api.Core.Models;
using qwitix_api.Core.Repositories;
using qwitix_api.Core.Services.OrganizerService.DTOs;

namespace qwitix_api.Core.Services.OrganizerService
{
    public class OrganizerService(
        IOrganizerRepository organizerRepository,
        IUserRepository userRepository,
        IMapper<CreateOrganizerDTO, Organizer> createOrganizerMapper,
        IMapper<ResponseOrganizerDTO, Organizer> responseOrganizerMapper
    )
    {
        private readonly IOrganizerRepository _organizerRepository = organizerRepository;
        private readonly IUserRepository _userRepository = userRepository;

        private readonly IMapper<CreateOrganizerDTO, Organizer> _createOrganizerMapper =
            createOrganizerMapper;
        private readonly IMapper<ResponseOrganizerDTO, Organizer> _responseOrganizerMapper =
            responseOrganizerMapper;

        public async Task Create(CreateOrganizerDTO organizerDTO)
        {
            _ =
                await _userRepository.GetById(organizerDTO.UserId)
                ?? throw new NotFoundException("User not found.");

            var organizer = _createOrganizerMapper.ToEntity(organizerDTO);

            await _organizerRepository.Create(organizer);
        }

        public async Task<IEnumerable<ResponseOrganizerDTO>> GetAll(int offset, int limit)
        {
            var organizers = await _organizerRepository.GetAll(offset, limit);

            return _responseOrganizerMapper.ToDtoList(organizers);
        }

        public async Task<ResponseOrganizerDTO> GetById(string id)
        {
            var organizer =
                await _organizerRepository.GetById(id)
                ?? throw new NotFoundException("Organizer not found.");

            return _responseOrganizerMapper.ToDto(organizer);
        }

        public async Task UpdateById(string id, UpdateOrganizerDTO organizerDTO)
        {
            var organizer =
                await _organizerRepository.GetById(id)
                ?? throw new NotFoundException("Organizer not found.");

            PatchHelper.ApplyPatch(organizerDTO, organizer);

            await _organizerRepository.UpdateById(id, organizer);
        }
    }
}
