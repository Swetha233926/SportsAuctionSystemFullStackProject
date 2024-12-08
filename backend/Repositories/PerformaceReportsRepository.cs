using SportsAuctionSystem.Data;
using SportsAuctionSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace SportsAuctionSystem.Repositories
{
    public interface IPerformanceReportsRepository
    {
        Task<IEnumerable<PerformanceReports>> GetReportsByPlayerIdAsync(int playerId);
        Task<IEnumerable<PerformanceReports>> GetReportsByAnalystIdAsync(int analystId);
        Task<PerformanceReports> AddReportAsync(PerformanceReports report);
        Task<bool> UpdateReportAsync(PerformanceReports report);
    }

    public class PerformanceReportsRepository : IPerformanceReportsRepository
    {
        private readonly AuctionDbContext _context;

        public PerformanceReportsRepository(AuctionDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PerformanceReports>> GetReportsByPlayerIdAsync(int playerId)
        {
            return await _context.PerformanceReports
                .Where(pr => pr.PlayerId == playerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<PerformanceReports>> GetReportsByAnalystIdAsync(int analystId)
        {
            return await _context.PerformanceReports
                .Where(pr => pr.AnalystId == analystId)
                .ToListAsync();
        }

        public async Task<PerformanceReports> AddReportAsync(PerformanceReports report)
        {
            _context.PerformanceReports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<bool> UpdateReportAsync(PerformanceReports report)
        {
            _context.PerformanceReports.Update(report);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
