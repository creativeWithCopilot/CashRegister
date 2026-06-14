using Microsoft.AspNetCore.Mvc;
using CashRegister.DTOs;
using CashRegister.DTOs.TransactionDTOs;
using CashRegister.Services;

namespace CashRegister.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrasactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        // Inject the TransactionService.
        public TrasactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // Creates a new order.
        // POST: api/Transaction/CreateTransaction
        [HttpPost("CreateTransaction")]
        public async Task<ActionResult<ApiResponse<TransactionResponseDTO>>> CreateTransaction([FromBody] TransactionCreateDTO transactionDto)
        {
            var response = await _transactionService.CreateTransactionAsync(transactionDto);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        // Retrieves an order by its ID.
        // GET: api/Orders/GetOrderById/{id}
        [HttpGet("GetTransactionById/{id}")]
        public async Task<ActionResult<ApiResponse<TransactionResponseDTO>>> GetTransactionById(int id)
        {
            var response = await _transactionService.GetTransactionByIdAsync(id);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        // Retrieves all orders.
        // GET: api/Orders/GetAllOrders
        [HttpGet("GetAllTransaction")]
        public async Task<ActionResult<ApiResponse<List<TransactionResponseDTO>>>> GetAllTransaction()
        {
            var response = await _transactionService.GetAllTransactionAsync();
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }
    }
}