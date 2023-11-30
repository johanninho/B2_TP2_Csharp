using Microsoft.EntityFrameworkCore;
using WebFincance.API.Models;

namespace WebFincance.API.Data;
public class FinanceContext : DbContext
{
    public FinanceContext(DbContextOptions<FinanceContext> options) : base(options)
    {
    }

    public DbSet<FinancialReport> FinancialReports { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<User> Users { get; set; }

   
}
