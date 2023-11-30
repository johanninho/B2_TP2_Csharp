using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebFincance.API.DTOs;
using WebFincance.API.Models;
using WebFincance.API.Data;

namespace WebFincance.API.Services;


public class ReportService : IReportService
{
    private readonly FinanceContext _context;

    public ReportService(FinanceContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FinancialReportDTO>> GetAllAsync()
    {
        var reports = await _context.FinancialReports
                                    .Select(r => new FinancialReportDTO
                                    {
                                        Id = r.Id,
                                        ReportDate = r.ReportDate,
                                        Content = r.Content,
                                        UserId = r.UserId
                                    })
                                    .ToListAsync();
        return reports;
    }

    public async Task<FinancialReportDTO> GetByIdAsync(int id)
    {
        var report = await _context.FinancialReports
                                   .Where(r => r.Id == id)
                                   .Select(r => new FinancialReportDTO
                                   {
                                       Id = r.Id,
                                       ReportDate = r.ReportDate,
                                       Content = r.Content,
                                       UserId = r.UserId
                                   })
                                   .FirstOrDefaultAsync();
        return report;
    }

    public async Task<FinancialReportDTO> CreateAsync(FinancialReportDTO reportDto)
    {
        var report = new FinancialReport
        {
            ReportDate = reportDto.ReportDate,
            Content = reportDto.Content,
            UserId = reportDto.UserId
        };

        _context.FinancialReports.Add(report);
        await _context.SaveChangesAsync();

        reportDto.Id = report.Id;
        return reportDto;
    }

    public async Task<bool> UpdateAsync(FinancialReportDTO reportDto)
    {
        var report = await _context.FinancialReports.FindAsync(reportDto.Id);
        if (report == null)
        {
            return false;
        }

        report.ReportDate = reportDto.ReportDate;
        report.Content = reportDto.Content;
        // Update other fields as necessary

        _context.FinancialReports.Update(report);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var report = await _context.FinancialReports.FindAsync(id);
        if (report == null)
        {
            return false;
        }

        _context.FinancialReports.Remove(report);
        await _context.SaveChangesAsync();
        return true;
    }
}
