using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PixelCost.Service.RecordingAPI.Models.DTOs;
using PixelCost.Service.RecordingAPI.Services.Interfaces;

namespace PixelCost.Service.RecordingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueController : ControllerBase
    {
        private readonly IRevenueRepository _revenueRepo;
        public RevenueController(IRevenueRepository revenueRepo)
        {
            _revenueRepo = revenueRepo;
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(RevenueDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveByUserIdAsync(string userId)
        {
            IList<RevenueDTO>? revenueDTOs = await _revenueRepo.RetrieveByUserIdAsync(userId);
            return (revenueDTOs != null) ? Ok(revenueDTOs) : NotFound();
        }

        [HttpGet("duration/{id:long}")]
        [ProducesResponseType(typeof(List<RevenueDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveByDurationIdAsync(long id)
        {
            IList<RevenueDTO>? revenueDTOs = await _revenueRepo.RetrieveByDurationIdAsync(id);
            return (revenueDTOs != null) ? Ok(revenueDTOs) : NotFound();
        }

        [HttpGet("subDuration/{id:long}")]
        [ProducesResponseType(typeof(List<RevenueDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveBySubDurationIdAsync(long id)
        {
            IList<RevenueDTO>? revenueDTOs = await _revenueRepo.RetrieveBySubDurationIdAsync(id);
            return (revenueDTOs != null) ? Ok(revenueDTOs) : NotFound();
        }


        [HttpGet("id/{id:long}", Name = nameof(RetrieveRevenueByIdAsync))]
        [ProducesResponseType(typeof(RevenueDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveRevenueByIdAsync(long id)
        {
            RevenueDTO? revenueDTO = await _revenueRepo.RetrieveByIdAsync(id);
            return (revenueDTO != null) ? Ok(revenueDTO) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateAsync([FromBody] RevenueDTO revenueDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (revenueDTO == null)
                return BadRequest(new ProblemDetails
                {
                    Title = "The entity to be updated cannot be null"
                });

            if (revenueDTO.Id != default)
                return BadRequest(new ProblemDetails
                {
                    Title = "The id property of the entity to be updated cannot be set, It should be the default value"
                });

            RevenueDTO? createdEntity = await _revenueRepo.CreateAsync(revenueDTO);

            return (createdEntity != null) ?
                CreatedAtRoute(
                    routeName: nameof(RetrieveRevenueByIdAsync),
                    routeValues: new { id = createdEntity.Id },
                    value: createdEntity) :
                StatusCode(500);
        }

        [HttpPatch]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateAsync([FromBody] RevenueDTO revenueDTO)
        {

            if (await _revenueRepo.RetrieveByIdAsync(revenueDTO.Id) == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(new ProblemDetails
                {
                    Title = "Model state is invalid"
                });

            if (revenueDTO == null)
                return BadRequest(new ProblemDetails
                {
                    Title = "The entity to be updated cannot be null"
                });

            RevenueDTO? updatedEntity = await _revenueRepo.UpdateAsync(revenueDTO);
            return (updatedEntity != null) ? NoContent() : StatusCode(500);
        }


        [HttpDelete("{id:long}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            if (await _revenueRepo.RetrieveByIdAsync(id) == null)
                return NotFound();
            bool result = await _revenueRepo.DeleteAsync(id);
            return (result) ? NoContent() : StatusCode(500);
        }

    }
}
