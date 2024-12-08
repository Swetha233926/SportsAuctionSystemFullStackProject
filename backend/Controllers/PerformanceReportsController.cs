using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsAuctionSystem.Models;
using SportsAuctionSystem.Services;

namespace SportsAuctionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceReportsController : ControllerBase
    {
        private readonly IPerformanceReportsService _performanceReportsService;

        public PerformanceReportsController(IPerformanceReportsService performanceReportsService)
        {
            _performanceReportsService = performanceReportsService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] PerformanceReports report)
        {
            var createdReport = await _performanceReportsService.CreatePerformanceReportAsync(report);
            return Ok(createdReport);
        }

        [HttpGet("player/{playerId}")]
        public async Task<IActionResult> GetReportsByPlayer(int playerId)
        {
            var reports = await _performanceReportsService.GetReportsByPlayerAsync(playerId);
            return Ok(reports);
        }

        [HttpGet("analyst/{analystId}")]
        public async Task<IActionResult> GetReportsByAnalyst(int analystId)
        {
            var reports = await _performanceReportsService.GetReportsByAnalystAsync(analystId);
            return Ok(reports);
        }

        [HttpPut("update/{reportId}")]
        public async Task<IActionResult> UpdateReport(int reportId, [FromBody] PerformanceReports report)
        {
            report.ReportId = reportId;
            var success = await _performanceReportsService.UpdatePerformanceReportAsync(report);
            if (!success) return NotFound("Performance report not found.");
            return NoContent();
        }
    }
}
