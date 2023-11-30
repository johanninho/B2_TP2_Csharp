using System.Collections.Generic;

namespace WebFincance.API.Models
{
    public class Portfolio
    {
        public int Id { get; set; }
        public decimal TotalValue { get; set; }
        // Vous pouvez avoir des propriétés supplémentaires pour décrire le portefeuille, comme le risque, les préférences, etc.

        // Clé étrangère pour l'utilisateur
        public int UserId { get; set; }

        // Relation de navigation
        public User User { get; set; }
    }
}
