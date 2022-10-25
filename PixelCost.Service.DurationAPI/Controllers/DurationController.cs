using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PixelCost.Service.DurationAPI.Models.DTOs;
using PixelCost.Service.DurationAPI.Services.Interfaces;
using System.Net;

namespace PixelCost.Service.DurationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DurationController : ControllerBase
    {

        private readonly IDurationRepository _durationRepository;

        public DurationController(IDurationRepository durationRepository) {
            _durationRepository = durationRepository;
        }


        [HttpGet("id/{id:long}")]
        [ProducesResponseType(typeof(DurationDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveByIdAsync(long id) {
            DurationDTO? durationDTO = await _durationRepository.RetrieveByIdAsync(id);
            return (durationDTO != null) ? Ok(durationDTO) : NotFound();
        }


        [HttpGet("userId/{userId}")]
        [ProducesResponseType(typeof(List<DurationDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveByUserIdAsync(string userId) {
            IList<DurationDTO>? durationDTOs = await _durationRepository.RetrieveByUserIdAsync(userId);
            return (durationDTOs != null) ? Ok(durationDTOs) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ProblemDetails),400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateAsync([FromBody] DurationDTO durationDTO) {

            if (durationDTO == null)
                return BadRequest(new ProblemDetails { 
                    Title = "The entity cannot be null"
                });

            if (durationDTO.Id != default)
                return BadRequest(new ProblemDetails { 
                    Title = "The new entity should not have specified id."
                });

            if (!ModelState.IsValid)
                return BadRequest(new ProblemDetails { 
                    Title = "Model status is not valid."
                });

            DurationDTO? durationDto = await _durationRepository.CreateAsync(durationDTO);

            return (durationDto != null) ? Created(nameof(RetrieveByIdAsync),durationDto) : StatusCode(500);

        }


        [HttpPatch("{id:long}")]
        [ProducesResponseType(typeof(DurationDTO),202)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ProblemDetails),400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateAsync([FromBody] DurationDTO durationDTO, long id) {

            if (durationDTO == null)
                return BadRequest();

            if (!ModelState.IsValid || id != durationDTO.Id)
                return BadRequest();

            if(!await _durationRepository.IsExists(id))
                return NotFound();

            DurationDTO? updatedEntiy = await _durationRepository.UpdateAsync(durationDTO);

            return (updatedEntiy != null) ? Accepted(updatedEntiy) : StatusCode(500);

        }


        [HttpDelete("{id:long}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteAsync(long id) {

            if (!await _durationRepository.IsExists(id))
                return NotFound();

            return (await _durationRepository.DeleteAsync(id)) ? NoContent() : StatusCode(500);

        }

    }
}
