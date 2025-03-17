using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using qwitix_api.Core.Services.OrganizerService;
using qwitix_api.Core.Services.OrganizerService.DTOs;
using qwitix_api.Core.Services.TicketService;
using qwitix_api.Core.Services.TicketService.DTOs;

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateOrganizerDTO organizerDTO)
        {
            try
            {
                await _organizerService.Create(organizerDTO);

                return Created();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("/organizers")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<ResponseOrganizerDTO>)
        )]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(int offset, int limit)
        {
            try
            {
                IEnumerable<ResponseOrganizerDTO> organizers = await _organizerService.GetAll(
                    offset,
                    limit
                );

                return Ok(organizers);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("/organizer/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseOrganizerDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                ResponseOrganizerDTO organizer = await _organizerService.GetById(id);

                return Ok(organizer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("/organizer/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateById(string id, UpdateOrganizerDTO organizerDTO)
        {
            try
            {
                await _organizerService.UpdateById(id, organizerDTO);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
