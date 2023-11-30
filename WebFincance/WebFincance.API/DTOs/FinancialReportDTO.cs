using System;

namespace WebFincance.API.DTOs
{
    public class FinancialReportDTO
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public object Id { get; internal set; }
    }
}
