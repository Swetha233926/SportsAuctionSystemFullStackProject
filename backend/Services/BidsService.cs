using System.Security.Claims;
using SportsAuctionSystem.Models;
using SportsAuctionSystem.Repositories;

namespace SportsAuctionSystem.Services
{
    public interface IBidsService
    {
        Task<Bids> PlaceBidAsync(Bids bid);
        Task<IEnumerable<Bids>> GetBidsForAuctionAsync(int auctionId);
        Task<Bids> GetHighestBidAsync(int auctionId, int playerId);
        Task<List<Bids>> GetBidsByStatusAsync(string status);
        Task<IEnumerable<Bids>> GetAllBidsAsync();
        Task<Bids> UpdateBidAsync(Bids bid);
    }

    public class BidsService : IBidsService
    {
        private readonly IBidsRepository _bidsRepository;
        private readonly IAuctionRepository _auctionRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamRepository _teamRepository;

        public BidsService(IBidsRepository bidsRepository, IAuctionRepository auctionRepository, IPlayerRepository playerRepository, ITeamRepository teamRepository)
        {
            _bidsRepository = bidsRepository;
            _auctionRepository = auctionRepository;
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
           
        }

        //get all bids
        public async Task<IEnumerable<Bids>> GetAllBidsAsync()
        {
            return await _bidsRepository.GetAllBidsAsync();
        }
        public async Task<Bids> PlaceBidAsync(Bids bid)
        {
            
            var auction = await _auctionRepository.GetAuctionByIdAsync(bid.AuctionId);
           
            if (auction.Status != "Ongoing")
            {
                throw new InvalidOperationException("Auction is not ongoing.");
            }

            var player = await _playerRepository.GetPlayerByIdAsync(bid.PlayerId);
            if (player == null)
            {
                throw new InvalidOperationException("Player id is not valid or does not exist");
            }
            player.CurrentBid = bid.BidAmount;

            var team = await _teamRepository.GetTeamByIdAsync(bid.TeamId);
            if (team.Budget< bid.BidAmount)
            {
                throw new InvalidOperationException("Bid amount is higher than total expenditure, no enough money");
            }
            if (team.Sport != player.Sport)
            {
                throw new InvalidOperationException("Team and player sport is different");
            }
            if (team == null)
            {
                throw new InvalidOperationException("Team id is incorrect");
            }

            
            // Validate bid amount and status
            var highestBid = await _bidsRepository.GetHighestBidAsync(bid.AuctionId, bid.PlayerId);
            if (highestBid != null && bid.BidAmount <= highestBid.BidAmount)
            {
                throw new InvalidOperationException("Bid amount must be higher than the current highest bid.");
            }

            // Add the new bid
            bid.Status = "Winning";
            var placedBid = await _bidsRepository.AddBidAsync(bid);

            // Update the previous highest bid to "Outbid"
            if (highestBid != null)
            {
                await _bidsRepository.UpdateBidStatusAsync(highestBid.BidId, "Outbid");
            }

            return placedBid;
        }

        public async Task<IEnumerable<Bids>> GetBidsForAuctionAsync(int auctionId)
        {
            return await _bidsRepository.GetBidsByAuctionIdAsync(auctionId);
        }

        public async Task<List<Bids>> GetBidsByStatusAsync(string status)
        {
            var validStatuses = new[] { "Active", "Outbid", "Winning" };

            if (!validStatuses.Contains(status))
            {
                throw new ArgumentException("Invalid status", nameof(status));
            }
            var bids = await _bidsRepository.GetBidsByStatusAsync(status);
            return bids;
        }

        public async Task<Bids> GetHighestBidAsync(int auctionId, int playerId)
        {
            return await _bidsRepository.GetHighestBidAsync(auctionId, playerId);
        }

        public async Task<Bids> UpdateBidAsync(Bids bid)
        {

            return await _bidsRepository.UpdateBidAsync(bid);
        }
    }

}
