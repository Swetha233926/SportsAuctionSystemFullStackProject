using SportsAuctionSystem.Data;
using SportsAuctionSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace SportsAuctionSystem.Repositories
{
    public interface IFinanceRepository
    {
        Task<IEnumerable<Finance>> GetAllFinances();
        Task<Finance> AddFinanceAsync(Finance finance);
        Task<IEnumerable<Finance>> GetTransactionsByTeamIdAsync(int teamId);
        Task<Finance> AddTransactionAsync(Finance finance);
        Task<bool> UpdateTransactionAsync(Finance finance);
    }

    public class FinanceRepository : IFinanceRepository
    {
        private readonly AuctionDbContext _context;

        public FinanceRepository(AuctionDbContext context)
        {
            _context = context;
        }
        //get all finances
        public async Task<IEnumerable<Finance>> GetAllFinances()
        {
            return await _context.Finances.ToListAsync();
        }

        //add transaction
        public async Task<Finance> AddFinanceAsync(Finance finance)
        {
            _context.Finances.Add(finance);
            await _context.SaveChangesAsync();
            return finance;
        }
        //get transaction by id 
        public async Task<IEnumerable<Finance>> GetTransactionsByTeamIdAsync(int teamId)
        {
            return await _context.Finances.Where(f => f.TeamId == teamId).ToListAsync();
        }

        //add transaction 
        public async Task<Finance> AddTransactionAsync(Finance finance)
        {
            _context.Finances.Add(finance);
            await _context.SaveChangesAsync();
            return finance;
        }

        //update transaction
        public async Task<bool> UpdateTransactionAsync(Finance finance)
        {
            _context.Finances.Update(finance);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
