using SportsAuctionSystem.Repositories;
using SportsAuctionSystem.Models;

namespace SportsAuctionSystem.Services
{
    public interface IContractsService
    {
        Task<Contracts> CreateContractAsync(Contracts contract);
        Task<IEnumerable<Contracts>> GetContractsByPlayerAsync(int playerId);
        Task<IEnumerable<Contracts>> GetContractsByTeamAsync(int teamId);
        Task<bool> UpdateContractAsync(Contracts contract);
        Task<bool> UpdateContractStatusAsync(int contractId, string newStatus);
    }

    public class ContractsService : IContractsService
    {
        private readonly IContractsRepository _contractsRepository;

        public ContractsService(IContractsRepository contractsRepository)
        {
            _contractsRepository = contractsRepository;
        }

        public async Task<Contracts> CreateContractAsync(Contracts contract)
        {
            return await _contractsRepository.AddContractAsync(contract);
        }

        public async Task<IEnumerable<Contracts>> GetContractsByPlayerAsync(int playerId)
        {
            return await _contractsRepository.GetContractsByPlayerIdAsync(playerId);
        }

        public async Task<IEnumerable<Contracts>> GetContractsByTeamAsync(int teamId)
        {
            return await _contractsRepository.GetContractsByTeamIdAsync(teamId);
        }

        public async Task<bool> UpdateContractAsync(Contracts contract)
        {
            return await _contractsRepository.UpdateContractAsync(contract);
        }

        public async Task<bool> UpdateContractStatusAsync(int contractId, string newStatus)
        {
            var contract = await _contractsRepository.GetByIdAsync(contractId); // Assuming GetByIdAsync is implemented in the repository
            if (contract == null) return false;

            contract.Status = newStatus; // Update the status
            await _contractsRepository.UpdateContractAsync(contract); // Save the updated contract
            return true;
        }
    }

}
