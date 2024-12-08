using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsAuctionSystem.Models;
using SportsAuctionSystem.Services;

namespace SportsAuctionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionResultsController : ControllerBase
    {
        private readonly IAuctionResultsService _auctionResultsService;

        public AuctionResultsController(IAuctionResultsService auctionResultsService)
        {
            _auctionResultsService = auctionResultsService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAuctionResult([FromBody] AuctionResults auctionResult)
        {
            var result = await _auctionResultsService.AddAuctionResultAsync(auctionResult);
            return Ok(result);
        }

        //Get all reports
        [HttpGet]
        public async Task<IActionResult> GetAllAuctionResults()
        {
            var results = await _auctionResultsService.GetAuctionResultsAsync();
            return Ok(results);
        }


        [HttpGet("auction/{auctionId}")]
        public async Task<IActionResult> GetAuctionResults(int auctionId)
        {
            var results = await _auctionResultsService.GetAuctionResultsByAuctionAsync(auctionId);
            if(results == null)
            {
                return NotFound();
            }
            return Ok(results);
        }

        [HttpGet("player/{playerId}")]
        public async Task<IActionResult> GetAuctionResultsByPlayer(int playerId)
        {
            var results = await _auctionResultsService.GetAuctionResultsByPlayerAsync(playerId);
            if (results == null)
            {
                return NotFound();
            }
            return Ok(results);
        }

        [HttpPut("update-status/{resultId}")]
        public async Task<IActionResult> UpdateAuctionResultStatus(int resultId, [FromBody] string status)
        {
            var success = await _auctionResultsService.UpdateAuctionResultStatusAsync(resultId, status);
            if (!success) return NotFound("Auction result not found.");
            return NoContent();
        }
    }
}
