using System;

namespace WebFincance.API.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } // Par exemple, "Deposit" ou "Withdrawal"
        public int UserId { get; set; }  // Clé étrangère pour l'utilisateur
    }
}
