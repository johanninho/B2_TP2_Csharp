using System;

namespace WebFincance.API.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } // Par exemple "deposit", "withdrawal"

        // Clé étrangère pour l'utilisateur
        public int UserId { get; set; }

        // Relation de navigation
        public User User { get; set; }
        public object Montant { get; internal set; }
        public object Description { get; internal set; }
        public int UtilisateurId { get; internal set; }
        public User Utilisateur { get; internal set; }
    }
}
