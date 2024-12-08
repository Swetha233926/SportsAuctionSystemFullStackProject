using SportsAuctionSystem.Models;
using SportsAuctionSystem.Repositories;

namespace SportsAuctionSystem.Services
{
    public interface ITeamService
    {
        Task<Team> GetTeamByIdAsync(int teamId);
        Task<IEnumerable<Team>> GetAllTeamsAsync();
        Task<Team> CreateTeamAsync(Team team);
        Task<Team> UpdateTeamAsync(Team team);
        Task<bool> DeleteTeamAsync(int teamId);
        Task<IEnumerable<Team>> GetTeamsByManagerIdAsync(int managerId);
    }

    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        //get team by id
        public async Task<Team> GetTeamByIdAsync(int teamId)
        {
            return await _teamRepository.GetTeamByIdAsync(teamId);
        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return await _teamRepository.GetAllTeamsAsync();
        }

        public async Task<Team> CreateTeamAsync(Team team)
        {
            // Validate that ManagerId corresponds to a valid Manager role
            if (!await _teamRepository.IsValidManagerAsync(team.ManagerId))
            {
                throw new InvalidOperationException("The specified ManagerId is invalid or does not belong to a Manager.");
            }

            var existingTeams = await _teamRepository.GetTeamsByManagerIdAsync(team.ManagerId);
            if (existingTeams != null && existingTeams.Any())
            {
                throw new InvalidOperationException("This Manager already has a team.");
            }

            // Add team to the database
            return await _teamRepository.AddTeamAsync(team);
        }

        public async Task<Team> UpdateTeamAsync(Team team)
        {
            if (!await _teamRepository.IsValidManagerAsync(team.ManagerId))
            {
                throw new InvalidOperationException("The specified ManagerId is invalid or does not belong to a Manager.");
            }

            return await _teamRepository.UpdateTeamAsync(team);
        }

        public async Task<bool> DeleteTeamAsync(int teamId)
        {
            return await _teamRepository.DeleteTeamAsync(teamId);
        }

        public async Task<IEnumerable<Team>> GetTeamsByManagerIdAsync(int managerId)
        {
            return await _teamRepository.GetTeamsByManagerIdAsync(managerId);
        }
    }

}
