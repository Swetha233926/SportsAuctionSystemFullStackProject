using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsAuctionSystem.Services;
using SportsAuctionSystem.Models;
using Microsoft.AspNetCore.Authorization;
using System.Numerics;
using System.Security.Claims;
namespace SportsAuctionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        //get player by id 
        [HttpGet("{id}")]
        [Authorize(Roles ="PlayerAgent")]
        public async Task<IActionResult> GetPlayerById(int id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        //get all players
        [HttpGet]
        public async Task<IActionResult> GetAllPlayers()
        {
            var players = await _playerService.GetAllPlayersAsync();
            return Ok(players);
        }

        //create players
        [HttpPost]
        [Authorize(Roles ="PlayerAgent")]
        public async Task<IActionResult> CreatePlayer([FromBody] Players player)
        {
            if (player == null)
            {
                return BadRequest("Player details are required.");
            }

            try
            {
                var createdPlayer = await _playerService.CreatePlayerAsync(player);
                return CreatedAtAction(nameof(GetPlayerById), new { id = createdPlayer.PlayerId }, createdPlayer);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //update players 
        [HttpPut("{id}")]
        [Authorize(Roles = "PlayerAgent")]
        public async Task<IActionResult> UpdatePlayer(int id, [FromBody] Players player)
        {
            if (player == null)
            {
                return BadRequest("Player ID mismatch.");
            }
            player.PlayerId = id;
            try
            {
                // Perform the update operation, the ID is already provided in the URL
                var updatedPlayer = await _playerService.UpdatePlayerAsync(player);
                return NoContent(); // Indicate that the update was successful
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        //delete player
        [HttpDelete("{id}")]
        [Authorize(Roles ="PlayerAgent")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var result = await _playerService.DeletePlayerAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        //get players by agent id 
        [HttpGet("agent/{agentId}")]
        public async Task<IActionResult> GetPlayersByAgent(int agentId)
        {
            var players = await _playerService.GetPlayersByAgentAsync(agentId);
            return Ok(players);
        }

        //get players by status 
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetPlayerByStatus(string status)
        {
            try
            {
                var players = await _playerService.GetPlayerByStatus(status);
                return Ok(players);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", error = ex.Message });
            }
        }

        //get players by sport 
        [HttpGet("sport/{sport}")]
        public async Task<IActionResult> GetPlayersBySport(string sport)
        {
            try
            {
                var players = await _playerService.GetPlayersBySport(sport);
                return Ok(players);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", error = ex.Message });
            }
        }

        [HttpGet("status/{status}/sport/{sport}")]
        public async Task<IActionResult> GetPlayersByStatusAndSport(string status, string sport)
        {
            try
            {
                if (string.IsNullOrEmpty(status) || string.IsNullOrEmpty(sport))
                {
                    return BadRequest(new { message = "Both status and sport parameters are required." });
                }

                var players = await _playerService.GetPlayersByStatusAndSport(status, sport);
                return Ok(players);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", error = ex.Message });
            }
        }




    }
}
