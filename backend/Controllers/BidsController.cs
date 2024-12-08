using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsAuctionSystem.Models;
using SportsAuctionSystem.Services;

namespace SportsAuctionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidsController : ControllerBase
    {
        private readonly IBidsService _bidsService;

        public BidsController(IBidsService bidsService)
        {
            _bidsService = bidsService;
        }

        //get all bids
        [HttpGet]
        public async Task<IActionResult> getBids()
        {
            var bids=await _bidsService.GetAllBidsAsync();
            return Ok(bids);
        }


        //place bids 
        [HttpPost]
        [Authorize(Roles = "TeamManager")]
        public async Task<IActionResult> PlaceBid([FromBody] Bids bid)
        {
            try
            {
                var placedBid = await _bidsService.PlaceBidAsync(bid);
                return Ok(placedBid);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //get bids by auction id, no authorization is required
        [HttpGet("auction/{auctionId}")]
      
        public async Task<IActionResult> GetBidsForAuction(int auctionId)
        {
            var bids = await _bidsService.GetBidsForAuctionAsync(auctionId);
            return Ok(bids);
        }

        //get highest bid by playerid,auction id 
        [HttpGet("highest/{auctionId}/{playerId}")]
        
        public async Task<IActionResult> GetHighestBid(int auctionId, int playerId)
        {
            var highestBid = await _bidsService.GetHighestBidAsync(auctionId, playerId);
            if (highestBid == null) return NotFound("No bids found.");
            return Ok(highestBid);
        }

        //get auctions by status 
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetBidsByStatus(string status)
        {
            try
            {
                var bids = await _bidsService.GetBidsByStatusAsync(status);
                return Ok(bids);  
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", error = ex.Message });
            }
        }
    }
}
