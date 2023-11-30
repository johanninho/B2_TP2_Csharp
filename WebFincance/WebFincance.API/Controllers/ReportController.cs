using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFincance.API.DTOs;
using WebFincance.API.Services;

namespace WebFincance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }

        // CRUD: Get all reports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinancialReportDTO>>> GetAllReports()
        {
            var reports = await _reportService.GetAllAsync();
            return Ok(reports);
        }

        // CRUD: Get report by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<FinancialReportDTO>> GetReportById(int id)
        {
            var report = await _reportService.GetByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }

        // CRUD: Create a new report
        [HttpPost]
        public async Task<ActionResult<FinancialReportDTO>> CreateReport([FromBody] FinancialReportDTO reportDto)
        {
            var newReport = await _reportService.CreateAsync(reportDto);
            return CreatedAtAction(nameof(GetReportById), new { id = newReport.Id }, newReport);
        }

        // CRUD: Update an existing report
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(int id, [FromBody] FinancialReportDTO reportDto)
        {
            if (id != reportDto.Id)
            {
                return BadRequest();
            }
            var updated = await _reportService.UpdateAsync(reportDto);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        // CRUD: Delete a report
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var deleted = await _reportService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Non-CRUD: Generate a consolidated report for all portfolios
        [HttpGet("consolidated")]
        public async Task<ActionResult<ConsolidatedReportDTO>> GenerateConsolidatedReport()
        {
            var report = await _reportService.GenerateConsolidatedReportAsync();
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }

        // Non-CRUD: Get reports within a specific date range
        [HttpGet("range")]
        public async Task<ActionResult<IEnumerable<FinancialReportDTO>>> GetReportsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var reports = await _reportService.GetByDateRangeAsync(startDate, endDate);
            if (reports == null)
            {
                return NotFound();
            }
            return Ok(reports);
        }
    }
}
