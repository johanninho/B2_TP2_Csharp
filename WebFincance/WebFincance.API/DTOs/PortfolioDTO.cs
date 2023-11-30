namespace WebFincance.API.DTOs
{
    public class PortfolioDTO
    {
        public int Id { get; set; }
        public decimal TotalValue { get; set; }
        // Ajoutez d'autres propriétés du portfolio que vous souhaitez exposer via l'API
        public int UserId { get; set; }  // Clé étrangère pour l'utilisateur
    }
}
