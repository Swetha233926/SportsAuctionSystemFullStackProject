using Microsoft.EntityFrameworkCore;
using SportsAuctionSystem.Data;
using SportsAuctionSystem.Models;

namespace SportsAuctionSystem.Repositories
{
    public interface IAuctionRepository
    {
        Task<Auction> GetAuctionByIdAsync(int auctionId);
        Task<IEnumerable<Auction>> GetAllAuctionsAsync();
        Task<Auction> AddAuctionAsync(Auction auction);
        Task<Auction> UpdateAuctionAsync(Auction auction);
        Task<bool> DeleteAuctionAsync(int auctionId);
        Task<IEnumerable<Auction>> GetAuctionsByAuctioneerIdAsync(int auctioneerId);
        Task<bool> IsValidAuctioneerAsync(int auctioneerId);
        Task UpdateAuctionStatusAsync(Auction auction);

        Task<List<Auction>> GetAuctionsByStatusAsync(string status);
    }

    public class AuctionRepository : IAuctionRepository
    {
        private readonly AuctionDbContext _context;

        public AuctionRepository(AuctionDbContext context)
        {
            _context = context;
        }

        public async Task<Auction> GetAuctionByIdAsync(int auctionId)
        {
            return await _context.Auctions.Include(a => a.User).FirstOrDefaultAsync(a => a.AuctionId == auctionId);
        }

        public async Task<IEnumerable<Auction>> GetAllAuctionsAsync()
        {
            return await _context.Auctions.Include(a => a.User).ToListAsync();
        }

        public async Task<Auction> AddAuctionAsync(Auction auction)
        {
            _context.Auctions.Add(auction);
            await _context.SaveChangesAsync();
            return auction;
        }

        public async Task<Auction> UpdateAuctionAsync(Auction auction)
        {
            _context.Auctions.Update(auction);
            await _context.SaveChangesAsync();
            return auction;
        }

        public async Task<bool> DeleteAuctionAsync(int auctionId)
        {
            var auction = await _context.Auctions.FindAsync(auctionId);
            if (auction == null) return false;

            _context.Auctions.Remove(auction);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Auction>> GetAuctionsByAuctioneerIdAsync(int auctioneerId)
        {
            return await _context.Auctions.Where(a => a.AuctioneerId == auctioneerId).ToListAsync();
        }

        public async Task<bool> IsValidAuctioneerAsync(int auctioneerId)
        {
            var user = await _context.Users.FindAsync(auctioneerId);
            return user != null && string.Equals(user.Role, "Auctioneer", StringComparison.OrdinalIgnoreCase);
        }

        public async Task UpdateAuctionStatusAsync(Auction auction)
        {
            _context.Auctions.Update(auction);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Auction>> GetAuctionsByStatusAsync(string status)
        {
            var auctions = await _context.Auctions.Where(a => a.Status == status).ToListAsync();
            return auctions;
        }
    }



}
