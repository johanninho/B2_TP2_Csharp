using System;
using System.Collections.Generic;
using System.Transactions;

namespace WebFincance.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        // Autres propriétés pour l'utilisateur comme l'adresse, téléphone, etc.

        // Relations
        public List<Transaction> Transactions { get; set; }
        public Portfolio Portfolio { get; set; }
        public object Nom { get; internal set; }
    }
}
