using Microsoft.AspNetCore.Mvc;
using qwitix_api.Core.Services.OrganizerService;
using qwitix_api.Core.Services.OrganizerService.DTOs;

namespace qwitix_api.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/")]
    public class OrganizerController : ControllerBase
    {
        private readonly OrganizerService _organizerService;

        public OrganizerController(OrganizerService organizerService)
        {
            _organizerService = organizerService;
        }

        [HttpPost("organizer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateOrganizerDTO organizerDTO)
        {
            await _organizerService.Create(organizerDTO);

            return Created();
        }

        [HttpGet("organizers")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<ResponseOrganizerDTO>)
        )]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(int offset, int limit)
        {
            IEnumerable<ResponseOrganizerDTO> organizers = await _organizerService.GetAll(
                offset,
                limit
            );

            return Ok(organizers);
        }

        [HttpGet("organizer/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseOrganizerDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            ResponseOrganizerDTO organizer = await _organizerService.GetById(id);

            return Ok(organizer);
        }

        [HttpPatch("organizer/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateById(string id, UpdateOrganizerDTO organizerDTO)
        {
            await _organizerService.UpdateById(id, organizerDTO);

            return Ok();
        }
    }
}
