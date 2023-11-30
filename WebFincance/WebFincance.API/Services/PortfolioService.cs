using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebFincance.API.DTOs;
using WebFincance.API.Models;
using WebFincance.API.Data;


namespace WebFincance.API.Services;

public class PortfolioService : IPortfolioService
{
    private readonly FinanceContext _context;

    public PortfolioService(FinanceContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PortfolioDTO>> GetAllAsync()
    {
        var portfolios = await _context.Portfolios
                                       .Select(p => new PortfolioDTO
                                       {
                                           Id = p.Id,
                                           TotalValue = p.TotalValue,
                                           UserId = p.UserId
                                       })
                                       .ToListAsync();
        return portfolios;
    }

    public async Task<PortfolioDTO> GetByIdAsync(int id)
    {
        var portfolio = await _context.Portfolios
                                      .Where(p => p.Id == id)
                                      .Select(p => new PortfolioDTO
                                      {
                                          Id = p.Id,
                                          TotalValue = p.TotalValue,
                                          UserId = p.UserId
                                      })
                                      .FirstOrDefaultAsync();
        return portfolio;
    }

    public async Task<PortfolioDTO> CreateAsync(PortfolioDTO portfolioDto)
    {
        var portfolio = new Portfolio
        {
            TotalValue = portfolioDto.TotalValue,
            UserId = portfolioDto.UserId
        };

        _context.Portfolios.Add(portfolio);
        await _context.SaveChangesAsync();

        portfolioDto.Id = portfolio.Id;
        return portfolioDto;
    }

    public async Task<bool> UpdateAsync(PortfolioDTO portfolioDto)
    {
        var portfolio = await _context.Portfolios.FindAsync(portfolioDto.Id);
        if (portfolio == null)
        {
            return false;
        }

        portfolio.TotalValue = portfolioDto.TotalValue;
        // Update other fields as necessary

        _context.Portfolios.Update(portfolio);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var portfolio = await _context.Portfolios.FindAsync(id);
        if (portfolio == null)
        {
            return false;
        }

        _context.Portfolios.Remove(portfolio);
        await _context.SaveChangesAsync();
        return true;
    }

    internal Task GetTotalValueAsync(int id)
    {
        throw new NotImplementedException();
    }

    internal Task CalculatePerformanceAsync(int id, DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }
}
