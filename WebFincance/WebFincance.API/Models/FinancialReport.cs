using System;

namespace WebFincance.API.Models
{
    public class FinancialReport
    {
        public int Id { get; set; }
        public DateTime ReportDate { get; set; }
        public string Content { get; set; } // Le contenu du rapport, pourrait être JSON, XML ou simplement un String long

        // Clé étrangère pour l'utilisateur ou le portefeuille, selon la logique de l'application
        public int UserId { get; set; }

        // Relation de navigation
        public User User { get; set; }
    }
}
