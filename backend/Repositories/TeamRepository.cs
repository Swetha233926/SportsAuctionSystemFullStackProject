using SportsAuctionSystem.Models;
using SportsAuctionSystem.Data;
using Microsoft.EntityFrameworkCore;
namespace SportsAuctionSystem.Repositories
{
    public interface ITeamRepository
    {
        Task<Team> GetTeamByIdAsync(int teamId);
        Task<IEnumerable<Team>> GetAllTeamsAsync();
        Task<Team> AddTeamAsync(Team team);
        Task<Team> UpdateTeamAsync(Team team);
        Task<bool> DeleteTeamAsync(int teamId);
        Task<IEnumerable<Team>> GetTeamsByManagerIdAsync(int managerId);
        Task<bool> IsValidManagerAsync(int managerId);
    }

    public class TeamRepository : ITeamRepository
    {
        private readonly AuctionDbContext _context;

        public TeamRepository(AuctionDbContext context)
        {
            _context = context;
        }

        //get teamby id
        public async Task<Team> GetTeamByIdAsync(int teamId)
        {
            return await _context.Teams.Include(t => t.User).FirstOrDefaultAsync(t => t.TeamId == teamId);
        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return await _context.Teams.Include(t => t.User).ToListAsync();
        }

        public async Task<Team> AddTeamAsync(Team team)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            return team;
        }

        public async Task<Team> UpdateTeamAsync(Team team)
        {
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
            return team;
        }

        public async Task<bool> DeleteTeamAsync(int teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            if (team == null) return false;

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Team>> GetTeamsByManagerIdAsync(int managerId)
        {
            return await _context.Teams.Where(t => t.ManagerId == managerId).ToListAsync();
        }

        public async Task<bool> IsValidManagerAsync(int managerId)
        {
            var user = await _context.Users.FindAsync(managerId);
            return user != null && string.Equals(user.Role, "TeamManager", StringComparison.OrdinalIgnoreCase);
        }
    }
}
