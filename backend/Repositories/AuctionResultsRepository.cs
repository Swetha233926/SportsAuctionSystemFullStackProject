using SportsAuctionSystem.Data;
using SportsAuctionSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace SportsAuctionSystem.Repositories
{
    public interface IAuctionResultsRepository
    {
        Task<IEnumerable<AuctionResults>> GetAllAuctionResultsAsync();
        Task<IEnumerable<AuctionResults>> GetAuctionResultsByAuctionIdAsync(int auctionId);
        Task<IEnumerable<AuctionResults>> GetAuctionResultsByPlayerIdAsync(int playerId);
        Task<IEnumerable<AuctionResults>> GetAuctionResultsByTeamIdAsync(int teamId);
        Task<AuctionResults> AddAuctionResultAsync(AuctionResults auctionResult);
        Task<bool> UpdateAuctionResultStatusAsync(int resultId, string status);
    }

    public class AuctionResultsRepository : IAuctionResultsRepository
    {
        private readonly AuctionDbContext _context;

        public AuctionResultsRepository(AuctionDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuctionResults>> GetAllAuctionResultsAsync()
        {
            return await _context.AuctionResults.ToListAsync();
        }
        public async Task<IEnumerable<AuctionResults>> GetAuctionResultsByAuctionIdAsync(int auctionId)
        {
            return await _context.AuctionResults
                .Where(ar => ar.AuctionId == auctionId)
                .ToListAsync();
        }

        public async Task<IEnumerable<AuctionResults>> GetAuctionResultsByPlayerIdAsync(int playerId)
        {
            return await _context.AuctionResults
                .Where(ar => ar.PlayerId == playerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<AuctionResults>> GetAuctionResultsByTeamIdAsync(int teamId)
        {
            return await _context.AuctionResults
                .Where(ar => ar.WinningTeamId == teamId)
                .ToListAsync();
        }

        public async Task<AuctionResults> AddAuctionResultAsync(AuctionResults auctionResult)
        {
            _context.AuctionResults.Add(auctionResult);
            await _context.SaveChangesAsync();
            return auctionResult;
        }

        public async Task<bool> UpdateAuctionResultStatusAsync(int resultId, string status)
        {
            var result = await _context.AuctionResults.FindAsync(resultId);
            if (result == null) return false;

            result.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
