using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebFincance.API.DTOs;
using WebFincance.API.Services;

namespace WebFincance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly PortfolioService _portfolioService;

        public PortfolioController(PortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        // CRUD: Get all portfolios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortfolioDTO>>> GetAllPortfolios()
        {
            var portfolios = await _portfolioService.GetAllAsync();
            return Ok(portfolios);
        }

        // CRUD: Get portfolio by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PortfolioDTO>> GetPortfolioById(int id)
        {
            var portfolio = await _portfolioService.GetByIdAsync(id);
            if (portfolio == null)
            {
                return NotFound();
            }
            return Ok(portfolio);
        }

        // CRUD: Create a new portfolio
        [HttpPost]
        public async Task<ActionResult<PortfolioDTO>> CreatePortfolio([FromBody] PortfolioDTO portfolioDto)
        {
            var newPortfolio = await _portfolioService.CreateAsync(portfolioDto);
            return CreatedAtAction(nameof(GetPortfolioById), new { id = newPortfolio.Id }, newPortfolio);
        }

        // CRUD: Update an existing portfolio
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePortfolio(int id, [FromBody] PortfolioDTO portfolioDto)
        {
            if (id != portfolioDto.Id)
            {
                return BadRequest();
            }
            var updated = await _portfolioService.UpdateAsync(portfolioDto);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        // CRUD: Delete a portfolio
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePortfolio(int id)
        {
            var deleted = await _portfolioService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Non-CRUD: Get the total value of a portfolio
        [HttpGet("{id}/value")]
        public async Task<ActionResult<decimal>> GetTotalValue(int id)
        {
            var value = await _portfolioService.GetTotalValueAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }

        
        [HttpGet("{id}/performance")]
        public async Task<ActionResult<PortfolioPerformanceDTO>> GetPortfolioPerformance(int id, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var performance = await _portfolioService.CalculatePerformanceAsync(id, startDate, endDate);
            if (performance == null)
            {
                return NotFound();
            }
            return Ok(performance);
        }

        // GET: api/portfolio/{id}/asset-allocation
        [HttpGet("{id}/asset-allocation")]
        public async Task<ActionResult<AssetAllocationDTO>> GetAssetAllocation(int id)
        {
            var allocation = await _portfolioService.GetAssetAllocationAsync(id);
            if (allocation == null)
            {
                return NotFound();
            }
            return Ok(allocation);
        }

    }
}
