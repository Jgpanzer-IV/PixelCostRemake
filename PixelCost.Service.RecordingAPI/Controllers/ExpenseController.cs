using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PixelCost.Service.RecordingAPI.Models.DTOs;
using PixelCost.Service.RecordingAPI.Services.Interfaces;

namespace PixelCost.Service.RecordingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseRepository _expenseRepo;
        public ExpenseController(IExpenseRepository expenseRepo)
        {
            _expenseRepo = expenseRepo;
        }



        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(ExpenseDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveByUserIdAsync(string userId)
        {
            IList<ExpenseDTO>? expenseDTOs = await _expenseRepo.RetrieveByUserIdAsync(userId);
            return (expenseDTOs != null) ? Ok(expenseDTOs) : NotFound();
        }

        [HttpGet("duration/{id:long}")]
        [ProducesResponseType(typeof(List<ExpenseDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveByDurationIdAsync(long id)
        {
            IList<ExpenseDTO>? expenseDTOs = await _expenseRepo.RetrieveByDurationIdAsync(id);
            return (expenseDTOs != null) ? Ok(expenseDTOs) : NotFound();
        }

        [HttpGet("subDuration/{id:long}")]
        [ProducesResponseType(typeof(List<ExpenseDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveBySubDurationIdAsync(long id)
        {
            IList<ExpenseDTO>? expenseDTOs = await _expenseRepo.RetrieveBySubDurationIdAsync(id);
            return (expenseDTOs != null) ? Ok(expenseDTOs) : NotFound();
        }

        [HttpGet("category/{id:long}")]
        [ProducesResponseType(typeof(List<ExpenseDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveByCategoryIdAsync(long id)
        {
            IList<ExpenseDTO>? expenseDTOs = await _expenseRepo.RetrieveByCategoryIdAsync(id);
            return (expenseDTOs != null) ? Ok(expenseDTOs) : NotFound();
        }


        [HttpGet("id/{id:long}", Name = nameof(RetrieveExpenseByIdAsync))]
        [ProducesResponseType(typeof(ExpenseDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveExpenseByIdAsync(long id)
        {
            ExpenseDTO? expenseDTO = await _expenseRepo.RetrieveByIdAsync(id);
            return (expenseDTO != null) ? Ok(expenseDTO) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateAsync([FromBody] ExpenseDTO expenseDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (expenseDTO == null)
                return BadRequest(new ProblemDetails
                {
                    Title = "The entity to be updated cannot be null"
                });

            if (expenseDTO.Id != default)
                return BadRequest(new ProblemDetails
                {
                    Title = "The id property of the entity to be updated cannot be set, It should be the default value"
                });

            ExpenseDTO? createdEntity = await _expenseRepo.CreateAsync(expenseDTO);

            return (createdEntity != null) ?
                CreatedAtRoute(
                    routeName: nameof(RetrieveExpenseByIdAsync),
                    routeValues: new { id = createdEntity.Id },
                    value: createdEntity) :
                StatusCode(500);
        }

        [HttpPatch]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateAsync([FromBody] ExpenseDTO categoryDTO)
        {

            if (await _expenseRepo.RetrieveByIdAsync(categoryDTO.Id) == null)
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

            ExpenseDTO? updatedEntity = await _expenseRepo.UpdateAsync(categoryDTO);
            return (updatedEntity != null) ? NoContent() : StatusCode(500);
        }


        [HttpDelete("{id:long}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteAsync(long id)
        {

            if (await _expenseRepo.RetrieveByIdAsync(id) == null)
                return NotFound();
            bool result = await _expenseRepo.DeleteAsync(id);
            return (result) ? NoContent() : StatusCode(500);
        }








    }
}
