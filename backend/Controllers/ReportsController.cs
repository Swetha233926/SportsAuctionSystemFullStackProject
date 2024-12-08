using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsAuctionSystem.Services;
using SportsAuctionSystem.Models;
namespace SportsAuctionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsService _reportsService;

        public ReportsController(IReportsService reportsService)
        {
            _reportsService = reportsService;
        }

        // Create a new report
        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] Reports report)
        {
            var createdReport = await _reportsService.CreateReportAsync(report);
            return Ok(createdReport);
        }

        //Get all reports
        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            var reports = await _reportsService.GetReportsAsync();
            return Ok(reports);
        }


        // Get all reports of a specific type
        [HttpGet("type/{reportType}")]
        public async Task<IActionResult> GetReportsByType(string reportType)
        {
            var reports = await _reportsService.GetReportsByTypeAsync(reportType);
            return Ok(reports);
        }

        // Update an existing report
        [HttpPut("update/{reportId}")]
        public async Task<IActionResult> UpdateReport(int reportId, [FromBody] Reports report)
        {
            report.ReportId = reportId;
            var success = await _reportsService.UpdateReportAsync(report);
            if (!success) return NotFound("Report not found.");
            return NoContent();
        }
    }
}
