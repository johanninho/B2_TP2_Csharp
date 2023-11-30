namespace WebFincance.API.DTOs
{
    public class PortfolioPerformanceDTO
    {
        public decimal StartingValue { get; set; }
        public decimal EndingValue { get; set; }
        public decimal PerformancePercentage { get; set; }
        public decimal AbsoluteChange { get; set; }
        
    }
}
