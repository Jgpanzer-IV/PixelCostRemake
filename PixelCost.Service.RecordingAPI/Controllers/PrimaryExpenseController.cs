using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PixelCost.Service.RecordingAPI.Models.DTOs;
using PixelCost.Service.RecordingAPI.Services.Interfaces;

namespace PixelCost.Service.RecordingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrimaryExpenseController : ControllerBase
    {
        private readonly IPrimaryExpenseRepository _primaryExpenseRepo;
        public PrimaryExpenseController(IPrimaryExpenseRepository categoryRepository)
        {
            _primaryExpenseRepo = categoryRepository;
        }


        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(PrimaryExpenseDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveByUserIdAsync(string userId)
        {
            IList<PrimaryExpenseDTO>? primaryExpenseDTOs = await _primaryExpenseRepo.RetrieveByUserIdAsync(userId);
            return (primaryExpenseDTOs != null) ? Ok(primaryExpenseDTOs) : NotFound();
        }

        [HttpGet("duration/{id:long}")]
        [ProducesResponseType(typeof(List<PrimaryExpenseDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveByDurationIdAsync(long id)
        {
            IList<PrimaryExpenseDTO>? primaryExpenseDTOs = await _primaryExpenseRepo.RetrieveByDurationIdAsync(id);
            return (primaryExpenseDTOs != null) ? Ok(primaryExpenseDTOs) : NotFound();
        }

        [HttpGet("subDuration/{id:long}")]
        [ProducesResponseType(typeof(List<PrimaryExpenseDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveBySubDurationIdAsync(long id)
        {
            IList<PrimaryExpenseDTO>? primaryExpenseDTOs = await _primaryExpenseRepo.RetrieveBySubDurationIdAsync(id);
            return (primaryExpenseDTOs != null) ? Ok(primaryExpenseDTOs) : NotFound();
        }

        [HttpGet("category/{id:long}")]
        [ProducesResponseType(typeof(List<PrimaryExpenseDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveByCategoryIdAsync(long id)
        {
            IList<PrimaryExpenseDTO>? primaryExpenseDTOs = await _primaryExpenseRepo.RetrieveByCategoryIdAsync(id);
            return (primaryExpenseDTOs != null) ? Ok(primaryExpenseDTOs) : NotFound();
        }

        [HttpGet("id/{id:long}", Name = nameof(RetrievePrimaryExpenseByIdAsync))]
        [ProducesResponseType(typeof(PrimaryExpenseDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrievePrimaryExpenseByIdAsync(long id)
        {
            PrimaryExpenseDTO? categoryDTO = await _primaryExpenseRepo.RetrieveByIdAsync(id);
            return (categoryDTO != null) ? Ok(categoryDTO) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateAsync([FromBody] PrimaryExpenseDTO primaryExpenseDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (primaryExpenseDTO == null)
                return BadRequest(new ProblemDetails
                {
                    Title = "The entity to be updated cannot be null"
                });

            if (primaryExpenseDTO.Id != default)
                return BadRequest(new ProblemDetails
                {
                    Title = "The id property of the entity to be updated cannot be set, It should be the default value"
                });

            PrimaryExpenseDTO? createdEntity = await _primaryExpenseRepo.CreateAsync(primaryExpenseDTO);

            return (createdEntity != null) ?
                CreatedAtRoute(
                    routeName: nameof(RetrievePrimaryExpenseByIdAsync),
                    routeValues: new { id = createdEntity.Id },
                    value: createdEntity) :
                StatusCode(500);
        }

        [HttpPatch]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateAsync([FromBody] PrimaryExpenseDTO categoryDTO)
        {

            if (await _primaryExpenseRepo.RetrieveByIdAsync(categoryDTO.Id) == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(new ProblemDetails
                {
                    Title = "Model state is invalid"
                });

            if (categoryDTO == null)
                return BadRequest(new ProblemDetails
                {
                    Title = "The entity to be updated cannot be null"
                });

            PrimaryExpenseDTO? updatedEntity = await _primaryExpenseRepo.UpdateAsync(categoryDTO);
            return (updatedEntity != null) ? NoContent() : StatusCode(500);
        }


        [HttpDelete("{id:long}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteAsync(long id)
        {

            if (await _primaryExpenseRepo.RetrieveByIdAsync(id) == null)
                return NotFound();
            bool result = await _primaryExpenseRepo.DeleteAsync(id);
            return (result) ? NoContent() : StatusCode(500);
        }

    }
}
