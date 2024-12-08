using Microsoft.EntityFrameworkCore;
using SportsAuctionSystem.Models;
using SportsAuctionSystem.Repositories;

namespace SportsAuctionSystem.Services
{
    public interface IAuctionService
    {
        Task<Auction> GetAuctionByIdAsync(int auctionId);
        Task<IEnumerable<Auction>> GetAllAuctionsAsync();
        Task<Auction> CreateAuctionAsync(Auction auction);
        Task<Auction> UpdateAuctionAsync(Auction auction);
        Task<bool> DeleteAuctionAsync(int auctionId);
        Task<IEnumerable<Auction>> GetAuctionsByAuctioneerIdAsync(int auctioneerId);
        Task UpdateAuctionStatusAsync(Auction auction);
        Task<List<Auction>> GetAuctionsByStatusAsync(string status);
    }

    public class AuctionService : IAuctionService
    {
        private readonly IAuctionRepository _auctionRepository;

        public AuctionService(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }

        public async Task<Auction> GetAuctionByIdAsync(int auctionId)
        {
            return await _auctionRepository.GetAuctionByIdAsync(auctionId);
        }

        public async Task<IEnumerable<Auction>> GetAllAuctionsAsync()
        {
            return await _auctionRepository.GetAllAuctionsAsync();
        }

        public async Task<Auction> CreateAuctionAsync(Auction auction)
        {
            // Validate that AuctioneerId corresponds to a valid Auctioneer
            if (!await _auctionRepository.IsValidAuctioneerAsync(auction.AuctioneerId))
            {
                throw new InvalidOperationException("The specified AuctioneerId is invalid or does not belong to an Auctioneer.");
            }

            return await _auctionRepository.AddAuctionAsync(auction);
        }

        public async Task<Auction> UpdateAuctionAsync(Auction auction)
        {
            // Validate that AuctioneerId corresponds to a valid Auctioneer
            if (!await _auctionRepository.IsValidAuctioneerAsync(auction.AuctioneerId))
            {
                throw new InvalidOperationException("The specified AuctioneerId is invalid or does not belong to an Auctioneer.");
            }

            return await _auctionRepository.UpdateAuctionAsync(auction);
        }

        public async Task<bool> DeleteAuctionAsync(int auctionId)
        {
            return await _auctionRepository.DeleteAuctionAsync(auctionId);
        }

        public async Task<IEnumerable<Auction>> GetAuctionsByAuctioneerIdAsync(int auctioneerId)
        {
            return await _auctionRepository.GetAuctionsByAuctioneerIdAsync(auctioneerId);
        }

        public async Task UpdateAuctionStatusAsync(Auction auction)
        {
            await _auctionRepository.UpdateAuctionStatusAsync(auction);
        }

        public async Task<List<Auction>> GetAuctionsByStatusAsync(string status)
        {
            var validStatuses = new[] { "Upcoming", "Ongoing", "Completed" };

            if (!validStatuses.Contains(status))
            {
                throw new ArgumentException("Invalid status", nameof(status));
            }
            var auctions = await _auctionRepository.GetAuctionsByStatusAsync(status);
            return auctions;
        }

    }

}
