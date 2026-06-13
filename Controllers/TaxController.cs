using Microsoft.AspNetCore.Mvc;
using CashRegister.DTOs;
using CashRegister.DTOs.TaxDTOs;
using CashRegister.Services;

namespace CashRegister.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly TaxService _taxService;

        // Injecting the TaxService
        public TaxController(TaxService taxService)
        {
            _taxService = taxService;
        }
        
        // Creates a new type of tax.
        [HttpPost("CreateTax")]
        public async Task<ActionResult<ApiResponse<TaxResponseDTO>>> CreateTax([FromBody] TaxCreateDTO taxDto)
        {
            var response = await _taxService.CreateTaxAsync(taxDto);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        // Retrieves a tax by ID.
        [HttpGet("GetTaxById/{id}")]
        public async Task<ActionResult<ApiResponse<TaxResponseDTO>>> GetCTaxById(int id)
        {
            var response = await _taxService.GetTaxByIdAsync(id);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        // Updates an existing tax.
        [HttpPut("UpdateTax")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UpdateTax([FromBody] TaxUpdateDTO taxDto)
        {
            var response = await _taxService.UpdateTaxAsync(taxDto);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        // Deletes a tax by ID.
        [HttpDelete("DeleteTax/{id}")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> DeleteTax(int id)
        {
            var response = await _taxService.DeleteTaxAsync(id);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        // Retrieves all tax.
        [HttpGet("GetAllTaxes")]
        public async Task<ActionResult<ApiResponse<List<TaxResponseDTO>>>> GetAllTaxes()
        {
            var response = await _taxService.GetAllTaxAsync();
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }
    }
}