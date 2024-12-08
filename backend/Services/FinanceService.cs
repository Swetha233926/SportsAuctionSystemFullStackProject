using SportsAuctionSystem.Models;
using SportsAuctionSystem.Repositories;
namespace SportsAuctionSystem.Services
{
    public interface IFinanceService
    {
        Task<Finance> CreateFinanceRecordAsync(Finance finance);
        Task<Finance> CreateTransactionAsync(Finance finance);
        Task<IEnumerable<Finance>> GetTransactionsByTeamAsync(int teamId);
        Task<decimal> GetTotalExpenditureAsync(int teamId);
        Task<bool> UpdateTransactionAsync(Finance finance);
        Task<IEnumerable<Finance>> GetAllFinances();
    }

    public class FinanceService : IFinanceService
    {
        private readonly IFinanceRepository _financeRepository;

        public FinanceService(IFinanceRepository financeRepository)
        {
            _financeRepository = financeRepository;
        }

        public async Task<Finance> CreateFinanceRecordAsync(Finance finance)
        {
            return await _financeRepository.AddFinanceAsync(finance); // Add the finance record
        }
        //get all finances
        public async Task<IEnumerable<Finance>> GetAllFinances()
        {
            return await _financeRepository.GetAllFinances();
        }

        //create transaction
        public async Task<Finance> CreateTransactionAsync(Finance finance)
        {
            return await _financeRepository.AddTransactionAsync(finance);
        }

        //get transaction 
        public async Task<IEnumerable<Finance>> GetTransactionsByTeamAsync(int teamId)
        {
            return await _financeRepository.GetTransactionsByTeamIdAsync(teamId);
        }

        //get total expenditure
        public async Task<decimal> GetTotalExpenditureAsync(int teamId)
        {
            var transactions = await _financeRepository.GetTransactionsByTeamIdAsync(teamId);
            return transactions.Where(t => t.TransactionType == "Purchase")
                               .Sum(t => t.Amount);
        }

        //update transaction
        public async Task<bool> UpdateTransactionAsync(Finance finance)
        {
            return await _financeRepository.UpdateTransactionAsync(finance);
        }

        //get remaining budget async
        public async Task<decimal> GetRemainingBudgetAsync(int teamId, decimal initialBudget)
        {
            var totalExpenditure = await GetTotalExpenditureAsync(teamId);
            return initialBudget - totalExpenditure;
        }
    }

}
