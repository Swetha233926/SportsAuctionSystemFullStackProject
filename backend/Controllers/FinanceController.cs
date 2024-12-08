using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsAuctionSystem.Models;
using SportsAuctionSystem.Services;

namespace SportsAuctionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanceController : ControllerBase
    {
        private readonly IFinanceService _financeService;

        public FinanceController(IFinanceService financeService)
        {
            _financeService = financeService;
        }

        //get all finances
        [HttpGet]
        public async Task<IActionResult> GetAllFinances()
        {
            var finances = await _financeService.GetAllFinances();
            return Ok(finances);
        }

        // Add a new transaction
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] Finance finance)
        {
            var createdTransaction = await _financeService.CreateTransactionAsync(finance);
            return Ok(createdTransaction);
        }

        // Get all transactions for a team
        [HttpGet("team/{teamId}")]
        public async Task<IActionResult> GetTransactionsByTeam(int teamId)
        {
            var transactions = await _financeService.GetTransactionsByTeamAsync(teamId);
            return Ok(transactions);
        }

        // Get total expenditure for a team
        [HttpGet("total-expenditure/{teamId}")]
        public async Task<IActionResult> GetTotalExpenditure(int teamId)
        {
            var totalExpenditure = await _financeService.GetTotalExpenditureAsync(teamId);
            return Ok(totalExpenditure);
        }

        // Update a financial transaction
        [HttpPut("update/{financeId}")]
        public async Task<IActionResult> UpdateTransaction(int financeId, [FromBody] Finance finance)
        {
            finance.FinanceId = financeId;
            var success = await _financeService.UpdateTransactionAsync(finance);
            if (!success) return NotFound("Finance transaction not found.");
            return NoContent();
        }


    }

}
