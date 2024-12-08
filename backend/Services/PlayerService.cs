using System.Numerics;
using SportsAuctionSystem.Models;
using SportsAuctionSystem.Repositories;
namespace SportsAuctionSystem.Services
{
    public interface IPlayerService
    {
        Task<Players> GetPlayerByIdAsync(int playerId);
        Task<IEnumerable<Players>> GetAllPlayersAsync();
        Task<Players> CreatePlayerAsync(Players player);
        Task<Players> UpdatePlayerAsync(Players player);
        Task<bool> DeletePlayerAsync(int playerId);
        Task<IEnumerable<Players>> GetPlayersByAgentAsync(int agentId);
        Task<IEnumerable<Players>> GetPlayerByStatus(string status);
        Task<IEnumerable<Players>> GetPlayersBySport(string sport);
        Task<IEnumerable<Players>> GetPlayersByStatusAndSport(string status, string sport);


   }

    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        //get player by id
        public async Task<Players> GetPlayerByIdAsync(int playerId)
        {
            return await _playerRepository.GetPlayerByIdAsync(playerId);
        }

        //get all players
        public async Task<IEnumerable<Players>> GetAllPlayersAsync()
        {
            return await _playerRepository.GetAllPlayersAsync();
        }

        //create player
        public async Task<Players> CreatePlayerAsync(Players player)
        {
            // Validate that AgentId corresponds to a valid PlayerAgent
            if (!await _playerRepository.IsValidAgentAsync(player.AgentId))
            {
                throw new InvalidOperationException("The specified AgentId is invalid or does not belong to a PlayerAgent.");
            }

            return await _playerRepository.AddPlayerAsync(player);
        }

        //update async
        public async Task<Players> UpdatePlayerAsync(Players player)
        {
            if (!await _playerRepository.IsValidAgentAsync(player.AgentId))
            {
                throw new InvalidOperationException("The specified AgentId is invalid or does not belong to a PlayerAgent.");
            }

            return await _playerRepository.UpdatePlayerAsync(player);
        }

        //delete a player
        public async Task<bool> DeletePlayerAsync(int playerId)
        {
            return await _playerRepository.DeletePlayerAsync(playerId);
        }

        //get all players by agent id 
        public async Task<IEnumerable<Players>> GetPlayersByAgentAsync(int agentId)
        {
            return await _playerRepository.GetPlayersByAgentIdAsync(agentId);
        }

        //get player details by status
        public async Task<IEnumerable<Players>> GetPlayerByStatus(string status)
        {
            return await _playerRepository.GetPlayerByStatus(status);
        }

        //get player details by sport
        public async Task<IEnumerable<Players>> GetPlayersBySport(string sport)
        {
            return await _playerRepository.GetPlayersBySport(sport);
        }

        //get player staus and by sport
        public async Task<IEnumerable<Players>> GetPlayersByStatusAndSport(string status, string sport)
        {
            return await _playerRepository.GetPlayersByStatusAndSport(status, sport);
        }

    }

}
