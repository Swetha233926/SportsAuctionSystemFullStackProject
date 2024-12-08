using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsAuctionSystem.Services;
using SportsAuctionSystem.Models;

namespace SportsAuctionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IContractsService _contractsService;

        public ContractsController(IContractsService contractsService)
        {
            _contractsService = contractsService;
        }

        // Create a new contract
        [HttpPost]
        public async Task<IActionResult> CreateContract([FromBody] Contracts contract)
        {
            var createdContract = await _contractsService.CreateContractAsync(contract);
            return Ok(createdContract);
        }

        // Get all contracts for a specific player
        [HttpGet("player/{playerId}")]
        public async Task<IActionResult> GetContractsByPlayer(int playerId)
        {
            var contracts = await _contractsService.GetContractsByPlayerAsync(playerId);
            return Ok(contracts);
        }

        // Get all contracts for a specific team
        [HttpGet("team/{teamId}")]
        public async Task<IActionResult> GetContractsByTeam(int teamId)
        {
            var contracts = await _contractsService.GetContractsByTeamAsync(teamId);
            return Ok(contracts);
        }

        // Update an existing contract
        [HttpPut("update/{contractId}")]
        public async Task<IActionResult> UpdateContract(int contractId, [FromBody] Contracts contract)
        {
            contract.ContractId = contractId;
            var success = await _contractsService.UpdateContractAsync(contract);
            if (!success) return NotFound("Contract not found.");
            return NoContent();
        }

        // Update an existing contract status
        [HttpPut("update-status/{contractId}")]
        public async Task<IActionResult> UpdateContractStatus(int contractId, [FromBody] string newStatus)
        {
            var success = await _contractsService.UpdateContractStatusAsync(contractId, newStatus);
            if (!success) return NotFound("Contract not found.");
            return NoContent();
        }
    }
}
