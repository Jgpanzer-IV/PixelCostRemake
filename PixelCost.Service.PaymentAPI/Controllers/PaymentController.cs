using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PixelCost.Service.PaymentAPI.Models.DTOs;
using PixelCost.Service.PaymentAPI.Services.Interfaces;

namespace PixelCost.Service.PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly IPaymentRepository _paymentRepository;
        public PaymentController(IPaymentRepository paymentMethod)
        {
            _paymentRepository = paymentMethod;
        }

        [HttpGet("userId/{userId}")]
        [ProducesResponseType(typeof(List<PaymentMethodDTO>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> RetrieveByUserIdAsync(string userId)
        {

            List<PaymentMethodDTO>? result = await _paymentRepository.RetrieveByUserIdAsync(userId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        [HttpGet("id/{id}",Name = nameof(RetrieveByIdAsync))]
        [ProducesResponseType(typeof(PaymentMethodDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrieveByIdAsync(long id)
        {
            PaymentMethodDTO? result = await _paymentRepository.RetrieveByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);

        }


        [HttpGet("expense/{id}")]
        [ProducesResponseType(typeof(List<ExpenseDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrievePaymentExpenseById(long id) {

            List<ExpenseDTO>? expenseDTOs = await _paymentRepository.RetrievePaymentExpenseById(id);

            if (expenseDTOs == null || expenseDTOs.Count == 0)
                return NotFound();

            return Ok(expenseDTOs);

        }


        [HttpGet("primaryExpense/{id}")]
        [ProducesResponseType(typeof(List<PrimaryExpenseDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrievePaymentPrimaryExpenseById(long id) {

            List<PrimaryExpenseDTO>? primaryExpenseDTOs = await _paymentRepository.RetrievePaymentPrimaryExpenseById(id);
            if (primaryExpenseDTOs == null || primaryExpenseDTOs.Count == 0)
                return NotFound();

            return Ok(primaryExpenseDTOs);
        }


        [HttpGet("revenue/{id}")]
        [ProducesResponseType(typeof(List<RevenueDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RetrievePaymentRevenueById(long id) {

            List<RevenueDTO>? revenueDTOs = await _paymentRepository.RetrievePaymentRevenueById(id);
            if (revenueDTOs == null || revenueDTOs.Count == 0)
                return NotFound();

            return Ok(revenueDTOs);
        }

       


        [HttpPost]
        [ProducesResponseType(typeof(PaymentMethodDTO),201)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateAsync([FromBody] PaymentMethodDTO paymentMethodDTO)
        {
            // Validate Bad request
            if (paymentMethodDTO == null)
                return BadRequest(new ProblemDetails { 
                    Title = "The entity cannot be null.",
                    Detail = "The entity is null here, So the server cannot create new entity from null entity."
                });

            else if (paymentMethodDTO.Id != default)
                return BadRequest(new ProblemDetails
                {
                    Title = "The property Id has specified.",
                    Detail = "The property Id shouldn't be specified, Because the server will automatically generate the value of the property Id that being created"
                });

            else if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Try to Create a new entity.
            PaymentMethodDTO? result = await _paymentRepository.CreateAsync(paymentMethodDTO);

            // Check the result of creating wheather it is success.
            if (result == null)
                return StatusCode(500);

            // Return status success at the retrieveById route with an id of the new entity.
            return CreatedAtRoute(routeName: nameof(RetrieveByIdAsync),routeValues: new { id = result.Id },value: result);
        }


        [HttpPatch("{id:long}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ProblemDetails),400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateAsync([FromBody] PaymentMethodDTO paymentMethodDTO, long id)
        {
            // Validate user error or bad request.
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else if (paymentMethodDTO == null)
                return BadRequest(new ProblemDetails { 
                    Title = "The entity cannot be null.",
                    Detail = "The entity to be updated cannot be null"
                });
            else if (id != paymentMethodDTO.Id)
                return BadRequest(new ProblemDetails { 
                    Title = "The id of the entity to be updated is not the same.",
                    Detail = "Cannot find the entity to update, Because the specified id is not the same of the entity to be updated."
                });

            // Check for Notfound error status.
            if  (! await _paymentRepository.IsExists(id))
                return NotFound();

            // Try to update, Return NoContent if it success.
            if (await _paymentRepository.UpdateAsync(paymentMethodDTO) != null)
                return NoContent();

            // Return the internal server error if it not success.
            return StatusCode(500);
        }



        [HttpDelete("{id:long}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteByIdAsync(long id)
        {
            if (!await _paymentRepository.IsExists(id))
                return NotFound();

            bool result = await _paymentRepository.DeleteAsync(id);

            return result ? NoContent() : StatusCode(500);
               

        }

    }
}
