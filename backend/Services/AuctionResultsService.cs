using SportsAuctionSystem.Models;
using SportsAuctionSystem.Repositories;

namespace SportsAuctionSystem.Services
{
    public interface IAuctionResultsService
    {
        Task<IEnumerable<AuctionResults>> GetAuctionResultsAsync();
        Task<AuctionResults> AddAuctionResultAsync(AuctionResults auctionResult);
        Task<IEnumerable<AuctionResults>> GetAuctionResultsByAuctionAsync(int auctionId);
        Task<IEnumerable<AuctionResults>> GetAuctionResultsByPlayerAsync(int playerId);
        Task<bool> UpdateAuctionResultStatusAsync(int resultId, string status);
    }

    public class AuctionResultsService : IAuctionResultsService
    {
        private readonly IAuctionResultsRepository _auctionResultsRepository;

        public AuctionResultsService(IAuctionResultsRepository auctionResultsRepository)
        {
            _auctionResultsRepository = auctionResultsRepository;
        }

        public async Task<IEnumerable<AuctionResults>> GetAuctionResultsAsync()
        {
            return await _auctionResultsRepository.GetAllAuctionResultsAsync();
        }
        public async Task<AuctionResults> AddAuctionResultAsync(AuctionResults auctionResult)
        {
            return await _auctionResultsRepository.AddAuctionResultAsync(auctionResult);
        }

        public async Task<IEnumerable<AuctionResults>> GetAuctionResultsByAuctionAsync(int auctionId)
        {
            return await _auctionResultsRepository.GetAuctionResultsByAuctionIdAsync(auctionId);
        }

        public async Task<IEnumerable<AuctionResults>> GetAuctionResultsByPlayerAsync(int playerId)
        {
            return await _auctionResultsRepository.GetAuctionResultsByPlayerIdAsync(playerId);
        }

        public async Task<bool> UpdateAuctionResultStatusAsync(int resultId, string status)
        {
            return await _auctionResultsRepository.UpdateAuctionResultStatusAsync(resultId, status);
        }
    }

}
