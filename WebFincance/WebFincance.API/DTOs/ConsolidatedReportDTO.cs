namespace WebFincance.API.DTOs
{
    public class ConsolidatedReportDTO
    {
        public DateTime ReportStartDate { get; set; }
        public DateTime ReportEndDate { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetIncome { get; set; } // TotalIncome - TotalExpenses
        public List<PortfolioPerformanceDTO> PortfolioPerformances { get; set; }
        public List<AssetAllocationDTO> AssetAllocations { get; set; }
        public decimal TotalAssets { get; set; } // Sum of all assets across portfolios
        public decimal TotalLiabilities { get; set; } // Sum of all liabilities
       
        
    }
}