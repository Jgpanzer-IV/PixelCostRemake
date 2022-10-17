using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PixelCost.Service.CategoryAPI.Models.DTOs;
using PixelCost.Service.CategoryAPI.Services.Interfaces;

namespace PixelCost.Service.CategoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepo = categoryRepository;
        }


        [HttpGet("{id:long}", Name = nameof(RetrieveByIdAsync))]
        [ProducesResponseType(typeof(CategoryDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveByIdAsync(long id) { 
            CategoryDTO? categoryDTO = await _categoryRepo.RetrieveByIdAsync(id);
            return (categoryDTO != null) ? Ok(categoryDTO) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateAsync([FromBody] CategoryDTO categoryDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            if (categoryDTO == null)
                return BadRequest(new ProblemDetails { 
                    Title = "The entity to be updated cannot be null"
                });

            if (categoryDTO.Id != default)
                return BadRequest(new ProblemDetails { 
                    Title = "The id property of the entity to be updated cannot be set, It should be the default value"
                });

            CategoryDTO? createdEntity = await _categoryRepo.CreateAsync(categoryDTO);

            return (createdEntity != null) ?
                CreatedAtRoute(
                    routeName: nameof(RetrieveByIdAsync),
                    routeValues:new { id = createdEntity.Id },
                    value: createdEntity) : 
                StatusCode(500);
        }

        [HttpPatch]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ProblemDetails),400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateAsync([FromBody] CategoryDTO categoryDTO) {

            if (await _categoryRepo.RetrieveByIdAsync(categoryDTO.Id) == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(new ProblemDetails {
                    Title = "Model state is invalid"
                });

            if (categoryDTO == null)
                return BadRequest(new ProblemDetails { 
                    Title = "The entity to be updated cannot be null"
                });

            CategoryDTO? updatedEntity = await _categoryRepo.UpdateAsync(categoryDTO);
            return (updatedEntity != null) ? NoContent() : StatusCode(500);
        }


        [HttpDelete("{id:long}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteAsync(long id) {

            if (await _categoryRepo.RetrieveByIdAsync(id) == null)
                return NotFound();
            bool result = await _categoryRepo.DeleteAsync(id);
            return (result) ? NoContent() : StatusCode(500);
        }


    }
}
