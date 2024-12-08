using SportsAuctionSystem.Repositories;
using SportsAuctionSystem.Models;

namespace SportsAuctionSystem.Services
{
    public interface IReportsService
    {
        Task<IEnumerable<Reports>> GetReportsAsync();
        Task<Reports> CreateReportAsync(Reports report);
        Task<IEnumerable<Reports>> GetReportsByTypeAsync(string reportType);
        Task<bool> UpdateReportAsync(Reports report);
    }

    public class ReportsService : IReportsService
    {
        private readonly IReportsRepository _reportsRepository;

        public ReportsService(IReportsRepository reportsRepository)
        {
            _reportsRepository = reportsRepository;
        }

        public async Task<IEnumerable<Reports>> GetReportsAsync()
        {
            return await _reportsRepository.GetAllReportsAsync();
        }
        public async Task<Reports> CreateReportAsync(Reports report)
        {
            return await _reportsRepository.AddReportAsync(report);
        }

        public async Task<IEnumerable<Reports>> GetReportsByTypeAsync(string reportType)
        {
            return await _reportsRepository.GetReportsByTypeAsync(reportType);
        }

        public async Task<bool> UpdateReportAsync(Reports report)
        {
            return await _reportsRepository.UpdateReportAsync(report);
        }
    }

}
