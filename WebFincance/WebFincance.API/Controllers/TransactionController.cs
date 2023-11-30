using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebFincance.API.Services;
using WebFincance.API.Models;
using WebFincance.API.DTOs;


namespace WebFincance.API.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionController : ControllerBase
{
    private static readonly List<User> _utilisateurs = new();
    private static long _idCounter = 1;
    private object _transactionService;

    [HttpGet]
    public ActionResult<IEnumerable<Transaction>> GetTransactions(int? utilisateurId = null)
    {
        if (utilisateurId.HasValue)
        {
            var transactionsForUser = _utilisateurs
                .FirstOrDefault(u => u.Id == utilisateurId.Value)?.Transactions;
            if (transactionsForUser == null || !transactionsForUser.Any())
            {
                return NotFound("Transactions non trouvées pour l'utilisateur spécifié");
            }
            return Ok(transactionsForUser);
        }
        // Retourne toutes les transactions de tous les utilisateurs
        return Ok(_utilisateurs.SelectMany(u => u.Transactions));
    }

    [HttpGet("{id}")]
    public ActionResult<Transaction> GetTransaction(int id)
    {
        var transaction = _utilisateurs
            .SelectMany(u => u.Transactions)
            .FirstOrDefault(t => t.Id == id);
        if (transaction == null)
        {
            return NotFound("Transaction non trouvée");
        }
        return Ok(transaction);
    }

    [HttpPost]
    public ActionResult<Transaction> CreateTransaction([FromBody] Transaction transaction)
    {
        var utilisateur = _utilisateurs.FirstOrDefault(u => u.Id == transaction.UtilisateurId);
        if (utilisateur == null)
        {
            return BadRequest("Utilisateur non valide");
        }

        transaction.Id = _utilisateurs.SelectMany(u => u.Transactions).Count() + 1;
        utilisateur.Transactions.Add(transaction);
        return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateTransaction(int id, [FromBody] Transaction updatedTransaction)
    {
        var transaction = _utilisateurs
            .SelectMany(u => u.Transactions)
            .FirstOrDefault(t => t.Id == id);
        if (transaction == null)
        {
            return NotFound("Transaction non trouvée");
        }

        transaction.Description = updatedTransaction.Description;
        transaction.Montant = updatedTransaction.Montant;

        return NoContent();  // 204 No Content
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteTransaction(int id)
    {
        User owner = null;
        Transaction transactionToRemove = null;
        foreach (var utilisateur in _utilisateurs)
        {
            transactionToRemove = utilisateur.Transactions.FirstOrDefault(t => t.Id == id);
            if (transactionToRemove != null)
            {
                owner = utilisateur;
                break;
            }
        }

        if (transactionToRemove == null)
        {
            return NotFound("Transaction non trouvée");
        }

        owner.Transactions.Remove(transactionToRemove);
        return NoContent(); // 204 No Content
    }

    [HttpGet("sum")]
    public ActionResult<double> GetTotalSum()
    {
        double total = _utilisateurs.Sum(u => u.Transactions.Sum(t => (double)t.Montant));
        return Ok(total);
    }

    [HttpGet("{utilisateurId}/transaction-count")]
    public ActionResult<int> GetTransactionCountForUser(int utilisateurId)
    {
        var utilisateur = _utilisateurs.FirstOrDefault(u => u.Id == utilisateurId);
        if (utilisateur == null)
        {
            return NotFound("Utilisateur non trouvé");
        }

        int transactionCount = utilisateur.Transactions.Count;
        return Ok(transactionCount);
    }

    [HttpGet("{utilisateurId}/latestTransaction")]
    public ActionResult<Transaction> GetLatestTransaction(int utilisateurId)
    {
        var utilisateur = _utilisateurs.FirstOrDefault(u => u.Id == utilisateurId);
        if (utilisateur == null || !utilisateur.Transactions.Any())
        {
            return NotFound("Utilisateur non trouvé ou sans transactions");
        }

        var latestTransaction = utilisateur.Transactions.OrderByDescending(t => t.Id).First();
        return Ok(latestTransaction);
    }

    [HttpGet("top/transactions")]
    public ActionResult<User> GetUtilisateurWithMostTransactions()
    {
        var utilisateur = _utilisateurs.OrderByDescending(u => u.Transactions.Count).FirstOrDefault();
        if (utilisateur == null)
        {
            return NotFound("Aucun utilisateur avec des transactions n'a été trouvé.");
        }
        return Ok(utilisateur);
    }

    [HttpPatch("{id}/email")]
    public ActionResult UpdateEmail(int id, [FromBody] string email)
    {
        var utilisateur = _utilisateurs.FirstOrDefault(u => u.Id == id);
        if (utilisateur == null)
        {
            return NotFound("Utilisateur non trouvé");
        }

        // Validez l'email ici si nécessaire
        utilisateur.Email = email;
        return NoContent();
    }

    
    [HttpGet("stats")]
    public async Task<ActionResult<TransactionStatsDTO>> GetTransactionStats([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var stats = await (double)_transactionService.GetStatsAsync(startDate, endDate);
        if (stats == null)
        {
            return NotFound("No transactions found for the specified period.");
        }
        return Ok(stats);
    }

}
