using Microsoft.AspNetCore.Mvc;
using qwitix_api.Core.Services.OrganizerService;
using qwitix_api.Core.Services.OrganizerService.DTOs;

namespace qwitix_api.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizerController : ControllerBase
    {
        private readonly OrganizerService _organizerService;

        public OrganizerController(OrganizerService organizerService)
        {
            _organizerService = organizerService;
        }

        [HttpPost("/organizer")]
        public async Task Create(CreateOrganizerDTO organizerDTO)
        {
            await _organizerService.Create(organizerDTO);
        }

        [HttpGet("/organizers")]
        public async Task<IEnumerable<ResponseOrganizerDTO>> GetAll(int offset, int limit)
        {
            return await _organizerService.GetAll(offset, limit);
        }

        [HttpGet("/organizer/{id}")]
        public async Task<ResponseOrganizerDTO> GetById(string id)
        {
            return await _organizerService.GetById(id);
        }

        [HttpPatch("/organizer/{id}")]
        public async Task UpdateById(string id, UpdateOrganizerDTO organizerDTO)
        {
            await _organizerService.UpdateById(id, organizerDTO);
        }
    }
}
