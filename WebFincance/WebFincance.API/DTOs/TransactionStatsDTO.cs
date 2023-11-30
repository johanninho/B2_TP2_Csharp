namespace WebFincance.API.DTOs
{
    public class TransactionStatsDTO
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public int TransactionCount { get; set; }
       
    }
}
