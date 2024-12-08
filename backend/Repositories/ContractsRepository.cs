using SportsAuctionSystem.Data;
using SportsAuctionSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace SportsAuctionSystem.Repositories
{
    public interface IContractsRepository
    {
        Task<IEnumerable<Contracts>> GetContractsByPlayerIdAsync(int playerId);
        Task<IEnumerable<Contracts>> GetContractsByTeamIdAsync(int teamId);
        Task<Contracts> AddContractAsync(Contracts contract);
        Task<bool> UpdateContractAsync(Contracts contract);
        Task<Contracts> GetByIdAsync(int contractId);
    }

    public class ContractsRepository : IContractsRepository
    {
        private readonly AuctionDbContext _context;

        public ContractsRepository(AuctionDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contracts>> GetContractsByPlayerIdAsync(int playerId)
        {
            return await _context.Contracts.Where(c => c.PlayerId == playerId).ToListAsync();
        }

        public async Task<IEnumerable<Contracts>> GetContractsByTeamIdAsync(int teamId)
        {
            return await _context.Contracts.Where(c => c.TeamId == teamId).ToListAsync();
        }

        public async Task<Contracts> AddContractAsync(Contracts contract)
        {
            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();
            return contract;
        }

        public async Task<bool> UpdateContractAsync(Contracts contract)
        {
            _context.Contracts.Update(contract);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Contracts> GetByIdAsync(int contractId)
        {
            return await _context.Contracts.FindAsync(contractId); // Find contract by ContractId
        }
    }

}
