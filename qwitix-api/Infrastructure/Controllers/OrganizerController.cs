using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("organizer", Name = "CreateOrganizer")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateOrganizerDTO organizerDTO)
        {
            var userId = User.FindFirst("user_id")?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            await _organizerService.Create(userId, organizerDTO);

            return Created();
        }

        [HttpGet("organizer/list", Name = "GetOrganizerList")]
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

        [HttpGet("organizer/{id}", Name = "GetOrganizer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseOrganizerDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            ResponseOrganizerDTO organizer = await _organizerService.GetById(id);

            return Ok(organizer);
        }

        [HttpPatch("organizer/{id}", Name = "UpdateOrganizer")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
