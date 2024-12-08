using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsAuctionSystem.Models;
using SportsAuctionSystem.Services;


namespace SportsAuctionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService _auctionService;

        public AuctionController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        //get auction by id
        [HttpGet("{id}")]
      
        public async Task<IActionResult> GetAuctionById(int id)
        {
            var auction = await _auctionService.GetAuctionByIdAsync(id);
            if (auction == null)
            {
                return NotFound();
            }
            return Ok(auction);
        }

        //get all auctions
        [HttpGet]
        [Authorize(Roles = "Auctioneer")]
        public async Task<IActionResult> GetAllAuctions()
        {
            var auctions = await _auctionService.GetAllAuctionsAsync();
            return Ok(auctions);
        }

        [HttpPost]
        [Authorize(Roles ="Auctioneer")]
        public async Task<IActionResult> CreateAuction([FromBody] Auction auction)
        {
            if (auction == null)
            {
                return BadRequest("Auction details are required.");
            }

            try
            {
                var createdAuction = await _auctionService.CreateAuctionAsync(auction);
                return CreatedAtAction(nameof(GetAuctionById), new { id = createdAuction.AuctionId }, createdAuction);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles ="Auctioneer")]
        public async Task<IActionResult> UpdateAuction(int id, [FromBody] Auction auction)
        {
            if (auction == null)
            {
                return BadRequest();
            }
            auction.AuctionId = id;
            try
            {
                var updatedAuction = await _auctionService.UpdateAuctionAsync(auction);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="Auctioneer")]
        public async Task<IActionResult> DeleteAuction(int id)
        {
            var result = await _auctionService.DeleteAuctionAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("auctioneer/{auctioneerId}")]
        [Authorize(Roles = "Auctioneer")]
        public async Task<IActionResult> GetAuctionsByAuctioneerId(int auctioneerId)
        {
            var auctions = await _auctionService.GetAuctionsByAuctioneerIdAsync(auctioneerId);
            return Ok(auctions);
        }

        //Get auctions by status
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetAuctionsByStatus(string status)
        {
            try
            {
                var auctions = await _auctionService.GetAuctionsByStatusAsync(status);
                return Ok(auctions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", error = ex.Message });
            }
        }
    }
}
