using SportsAuctionSystem.Models;
using SportsAuctionSystem.Repositories;

namespace SportsAuctionSystem.Services
{
    public interface IPerformanceReportsService
    {
        Task<PerformanceReports> CreatePerformanceReportAsync(PerformanceReports report);
        Task<IEnumerable<PerformanceReports>> GetReportsByPlayerAsync(int playerId);
        Task<IEnumerable<PerformanceReports>> GetReportsByAnalystAsync(int analystId);
        Task<bool> UpdatePerformanceReportAsync(PerformanceReports report);
    }

    public class PerformanceReportsService : IPerformanceReportsService
    {
        private readonly IPerformanceReportsRepository _performanceReportsRepository;

        public PerformanceReportsService(IPerformanceReportsRepository performanceReportsRepository)
        {
            _performanceReportsRepository = performanceReportsRepository;
        }

        public async Task<PerformanceReports> CreatePerformanceReportAsync(PerformanceReports report)
        {
            return await _performanceReportsRepository.AddReportAsync(report);
        }

        public async Task<IEnumerable<PerformanceReports>> GetReportsByPlayerAsync(int playerId)
        {
            return await _performanceReportsRepository.GetReportsByPlayerIdAsync(playerId);
        }

        public async Task<IEnumerable<PerformanceReports>> GetReportsByAnalystAsync(int analystId)
        {
            return await _performanceReportsRepository.GetReportsByAnalystIdAsync(analystId);
        }

        public async Task<bool> UpdatePerformanceReportAsync(PerformanceReports report)
        {
            return await _performanceReportsRepository.UpdateReportAsync(report);
        }
    }

}
