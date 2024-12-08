using SportsAuctionSystem.Data;
using SportsAuctionSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace SportsAuctionSystem.Repositories
{
    public interface IBidsRepository
    {
        Task<IEnumerable<Bids>> GetBidsByAuctionIdAsync(int auctionId);
        Task<IEnumerable<Bids>> GetBidsByPlayerIdAsync(int playerId);
        Task<IEnumerable<Bids>> GetBidsByTeamIdAsync(int teamId);
        Task<Bids> AddBidAsync(Bids bid);
        Task<List<Bids>> GetBidsByStatusAsync(string status);
        Task<bool> UpdateBidStatusAsync(int bidId, string status);
        Task<Bids> GetHighestBidAsync(int auctionId, int playerId);
        Task<IEnumerable<Bids>> GetAllBidsAsync();
        Task<Bids> UpdateBidAsync(Bids bid);
    }

    public class BidsRepository : IBidsRepository
    {
        private readonly AuctionDbContext _context;

        public BidsRepository(AuctionDbContext context)
        {
            _context = context;
        }

        //get all bids
        public async Task<IEnumerable<Bids>> GetAllBidsAsync()
        {
            return await _context.Bids.ToListAsync();
        }

        //get by id
        public async Task<IEnumerable<Bids>> GetBidsByAuctionIdAsync(int auctionId)
        {
            return await _context.Bids.Where(b => b.AuctionId == auctionId).ToListAsync();
        }

        public async Task<IEnumerable<Bids>> GetBidsByPlayerIdAsync(int playerId)
        {
            return await _context.Bids.Where(b => b.PlayerId == playerId).ToListAsync();
        }

        public async Task<IEnumerable<Bids>> GetBidsByTeamIdAsync(int teamId)
        {
            return await _context.Bids.Where(b => b.TeamId == teamId).ToListAsync();
        }

        public async Task<Bids> AddBidAsync(Bids bid)
        {
            _context.Bids.Add(bid);
            await _context.SaveChangesAsync();
            return bid;
        }

        public async Task<List<Bids>> GetBidsByStatusAsync(string status)
        {

            var bids = await _context.Bids.Where(b => b.Status == status).ToListAsync(); 
            return bids;
        }

        public async Task<bool> UpdateBidStatusAsync(int bidId, string status)
        {
            var bid = await _context.Bids.FindAsync(bidId);
            if (bid == null) return false;

            bid.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Bids> GetHighestBidAsync(int auctionId, int playerId)
        {
            return await _context.Bids
                .Where(b => b.AuctionId == auctionId && b.PlayerId == playerId)
                .OrderByDescending(b => b.BidAmount)
                .FirstOrDefaultAsync();
        }

        public async Task<Bids> UpdateBidAsync(Bids bid)
        {
            _context.Bids.Update(bid);
            await _context.SaveChangesAsync();
            return bid;
        }
    }

}
