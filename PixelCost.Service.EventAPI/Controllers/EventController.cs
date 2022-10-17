using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PixelCost.Service.EventAPI.Database;
using PixelCost.Service.EventAPI.Models.DTOs;
using PixelCost.Service.EventAPI.Services.Interfaces;
using System.Runtime.CompilerServices;

namespace PixelCost.Service.EventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly ISubDurationRepository _subDurationRepository;

        public EventController(ISubDurationRepository subDurationRepository)
        {
            _subDurationRepository = subDurationRepository;
        }

        [HttpGet("id/{id:long}")]
        [ProducesResponseType(typeof(SubDurationDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveByIdAsync(long id){

            SubDurationDTO? subDurationDTO = await _subDurationRepository.RetrieveByIdAsync(id);
            return (subDurationDTO != null) ? Ok(subDurationDTO) : NotFound();
        }


        [HttpGet("userId/{userId}")]
        [ProducesResponseType(typeof(List<SubDurationDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveByUserIdAsync(string userId) {
        
            IList<SubDurationDTO>? subDurationDTOs = await _subDurationRepository.RetrieveByUserIdAsync(userId);
            return (subDurationDTOs != null) ? Ok(subDurationDTOs) : NotFound(); 
        
        }


        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateAsync([FromBody] SubDurationDTO subDurationDTO) {

            if (subDurationDTO == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            if (subDurationDTO.Id != default)
                return BadRequest();

            SubDurationDTO? created = await _subDurationRepository.CreateAsync(subDurationDTO);
            return (created != null) ? Created(nameof(RetrieveByIdAsync), created):StatusCode(500);
        }


        [HttpPatch]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ProblemDetails),400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateAsync([FromBody] SubDurationDTO subDurationDTO)
        {

            if (subDurationDTO == null)
                return BadRequest(new ProblemDetails
                {
                    Title = "The entity cannot be null."
                });

            if (!ModelState.IsValid)
                return BadRequest(new ProblemDetails
                {
                    Title = "The entity has incorrect format to be operate."
                });

            if (await _subDurationRepository.RetrieveByIdAsync(subDurationDTO.Id) == null)
                return NotFound();

            SubDurationDTO? created = await _subDurationRepository.UpdateAsync(subDurationDTO);
            return (created != null) ? NoContent() : StatusCode(500);
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteAsync(long id) {

            if (await _subDurationRepository.RetrieveByIdAsync(id) == null)
                return NotFound();

            return (await _subDurationRepository.DeleteAsync(id)) ? NoContent() : StatusCode(500);

        }

    }
}
