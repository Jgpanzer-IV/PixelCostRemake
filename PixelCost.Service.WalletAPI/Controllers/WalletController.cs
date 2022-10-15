using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PixelCost.Service.WalletAPI.Model.DTOs;
using PixelCost.Service.WalletAPI.Services.Interfaces;

namespace PixelCost.Service.WalletAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {

        private readonly IWalletRepository _walletRepository;

        public WalletController(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }


        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(WalletDTO), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> RetrieveById(string userId) {
           
            WalletDTO? walletDTO = await _walletRepository.RetrieveUpdatedByIdAsync(userId);

            if (walletDTO == null)
                return NotFound(new ProblemDetails() { 
                    Title = "Not found the entity of the specified id."
                });

            return Ok(walletDTO);
        }


        [HttpPost]
        [ProducesResponseType(typeof(WalletDTO), 201)]
        [ProducesResponseType(typeof(ProblemDetails),400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateWallet([FromBody] WalletDTO walletDto) {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (walletDto == null)
                return BadRequest(new ProblemDetails { 
                    Title = "The entity cannot be null."
                });

            if (await _walletRepository.RetrieveByIdAsync(walletDto.UserID) != null)
                return BadRequest(new ProblemDetails { 
                    Title = "There is entity that has already use the same key.",
                    Detail = "The given entity to be created use the same key as the entity that have been created."
                });

            WalletDTO? result = await _walletRepository.CreateAsync(walletDto);

            if (result == null) {
                return StatusCode(500);
            }
            return StatusCode(201,result);
        }


        [HttpPatch]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateWallet([FromBody] WalletDTO walletDto)
        {
            if (walletDto == null)
                return BadRequest(new ProblemDetails{ 
                    Title = "The entity to be update cannot be null."
                });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            WalletDTO? exists = await _walletRepository.RetrieveByIdAsync(walletDto.UserID);

            if (exists == null)
                return NotFound(new ProblemDetails
                {
                    Title = "The entity to be updated not found."
                });

            // Make property unchangable
            exists.UserID = walletDto.UserID;

            WalletDTO? result = await _walletRepository.UpdateAsync(walletDto);

            if (result == null)
                return StatusCode(500);

            return NoContent();
        }


        [HttpDelete("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ProblemDetails),400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(ProblemDetails),404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteWallet(string userId) {

            if (string.IsNullOrEmpty(userId))
                return BadRequest(new ProblemDetails {
                    Title = "Please specify the userId"
                });

            if (await _walletRepository.RetrieveByIdAsync(userId) == null)
                return NotFound(new ProblemDetails { 
                    Title = "The specified userid dosn't found to delete."
                });
            
            bool result = await _walletRepository.DeleteAsync(userId);

            return (result) ? NoContent() : StatusCode(500);
        }
    }
}
