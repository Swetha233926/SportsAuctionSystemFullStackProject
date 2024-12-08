using System.Numerics;
using Microsoft.EntityFrameworkCore;
using SportsAuctionSystem.Data;
using SportsAuctionSystem.Models;
namespace SportsAuctionSystem.Repositories
{
    public interface IPlayerRepository
    {
        Task<Players> GetPlayerByIdAsync(int playerId);
        Task<IEnumerable<Players>> GetAllPlayersAsync();
        Task<Players> AddPlayerAsync(Players player);
        Task<Players> UpdatePlayerAsync(Players player);
        Task<bool> DeletePlayerAsync(int playerId);
        Task<IEnumerable<Players>> GetPlayersByAgentIdAsync(int agentId);
        Task<bool> IsValidAgentAsync(int agentId);
        Task<IEnumerable<Players>> GetPlayerByStatus(string status);
        Task<IEnumerable<Players>> GetPlayersBySport(string sport);
        Task<IEnumerable<Players>> GetPlayersByStatusAndSport(string status, string sport);
    }

    public class PlayerRepository : IPlayerRepository
    {
        private readonly AuctionDbContext _context;

        public PlayerRepository(AuctionDbContext context)
        {
            _context = context;
        }

        //get player by id
        public async Task<Players> GetPlayerByIdAsync(int playerId)
        {
            return await _context.Players.Include(p => p.User).FirstOrDefaultAsync(p => p.PlayerId == playerId);
        }

        //get all players
        public async Task<IEnumerable<Players>> GetAllPlayersAsync()
        {
            return await _context.Players.Include(p => p.User).ToListAsync();
        }

        //add players
        public async Task<Players> AddPlayerAsync(Players player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return player;
        }

        //update player
        public async Task<Players> UpdatePlayerAsync(Players player)
        {
            _context.Players.Update(player);
            await _context.SaveChangesAsync();
            return player;
        }

        //delete player
        public async Task<bool> DeletePlayerAsync(int playerId)
        {
            var player = await _context.Players.FindAsync(playerId);
            if (player == null) return false;

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return true;
        }

        //get playeys by agent id 
        public async Task<IEnumerable<Players>> GetPlayersByAgentIdAsync(int agentId)
        {
            return await _context.Players.Where(p => p.AgentId == agentId).ToListAsync();
        }

        //valid agent are not 
        public async Task<bool> IsValidAgentAsync(int agentId)
        {
            var user = await _context.Users.FindAsync(agentId);
            return user != null && string.Equals(user.Role, "PlayerAgent", StringComparison.OrdinalIgnoreCase);
        }

        //get player by status
        public async Task<IEnumerable<Players>> GetPlayerByStatus(string status)
        {
            var players = await _context.Players.Where(b => b.Status == status).ToListAsync();
            return players;
        }

        //get players by sport
        public async Task<IEnumerable<Players>> GetPlayersBySport(string sport)
        {
            var players = await _context.Players.Where(p=>p.Sport == sport).ToListAsync();
            return players;
        }

        //get players by status and sport 
        public async Task<IEnumerable<Players>> GetPlayersByStatusAndSport(string status, string sport)
        {
            if (string.IsNullOrEmpty(status) || string.IsNullOrEmpty(sport))
                throw new ArgumentException("Status and Sport cannot be null or empty");

            return await _context.Players
                .Where(p => p.Status == status && p.Sport == sport)
                .ToListAsync();
        }

    }

}
