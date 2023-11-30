namespace WebFincance.API.DTOs
{
    public class AssetAllocationDTO
    {
        public decimal StocksPercentage { get; set; }
        public decimal BondsPercentage { get; set; }
        public decimal RealEstatePercentage { get; set; }
        public decimal CashPercentage { get; set; }
        public decimal OtherAssetsPercentage { get; set; }
        /
    }
}
