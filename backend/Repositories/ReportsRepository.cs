using SportsAuctionSystem.Data;
using SportsAuctionSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace SportsAuctionSystem.Repositories
{
    public interface IReportsRepository
    {
        Task<IEnumerable<Reports>> GetAllReportsAsync();
        Task<IEnumerable<Reports>> GetReportsByTypeAsync(string reportType);
        Task<Reports> AddReportAsync(Reports report);
        Task<bool> UpdateReportAsync(Reports report);
    }

    public class ReportsRepository : IReportsRepository
    {
        private readonly AuctionDbContext _context;

        public ReportsRepository(AuctionDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reports>> GetAllReportsAsync()
        {
            return await _context.Reports.ToListAsync();
        }
        public async Task<IEnumerable<Reports>> GetReportsByTypeAsync(string reportType)
        {
            return await _context.Reports.Where(r => r.Type == reportType).ToListAsync();
        }

        public async Task<Reports> AddReportAsync(Reports report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<bool> UpdateReportAsync(Reports report)
        {
            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
