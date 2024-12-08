using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsAuctionSystem.Models;
using SportsAuctionSystem.Services;

namespace SportsAuctionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        //get team by id
        [HttpGet("{id}")]
        [Authorize(Roles ="TeamManager")]
        public async Task<IActionResult> GetTeamById(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return Ok(team);
        }

        //get all teams
        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            return Ok(teams);
        }

        //create a team 
        [HttpPost]
        [Authorize(Roles ="TeamManager")]
        public async Task<IActionResult> CreateTeam([FromBody] Team team)
        {
            if (team == null)
            {
                return BadRequest();
            }

            try
            {
                var createdTeam = await _teamService.CreateTeamAsync(team);
                return CreatedAtAction(nameof(GetTeamById), new { id = createdTeam.TeamId }, createdTeam);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //update team details
        [HttpPut("{id}")]
        [Authorize(Roles = "TeamManager")]
        public async Task<IActionResult> UpdateTeam(int id, [FromBody] Team team)
        {
            if (team == null)
            {
                return BadRequest();
            }
            team.TeamId = id;
            try
            {
                var updatedTeam = await _teamService.UpdateTeamAsync(team);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //delete a team
        [HttpDelete("{id}")]
        [Authorize(Roles = "TeamManager")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var result = await _teamService.DeleteTeamAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        //get teams by manager id 
        [HttpGet("manager/{managerId}")]
        [Authorize(Roles = "TeamManager")]
        public async Task<IActionResult> GetTeamsByManagerId(int managerId)
        {
            var teams = await _teamService.GetTeamsByManagerIdAsync(managerId);
            return Ok(teams);
        }
    }
}

