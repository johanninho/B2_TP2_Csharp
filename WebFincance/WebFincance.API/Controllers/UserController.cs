using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebFincance.API.Models;
using WebFincance.API.DTOs;

[ApiController]
[Route("api/utilisateurs")]
public class UserController : ControllerBase
{
    private static readonly List<User> _utilisateurs = new();
    private static long _idCounter = 1;
    private object _userService;

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUtilisateurs()
    {
        return Ok(_utilisateurs);
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUtilisateur(int id)
    {
        var utilisateur = _utilisateurs.FirstOrDefault(u => u.Id == id);
        if (utilisateur == null)
        {
            return NotFound("Utilisateur non trouvé");
        }
        return Ok(utilisateur);
    }

    [HttpPost]
    public ActionResult<User> CreateUtilisateur([FromBody] User utilisateur)
    {
        utilisateur.Id = _utilisateurs.Count + 1;

        // Si l'utilisateur a des transactions, générez des IDs pour elles aussi
        int transactionId = _utilisateurs.SelectMany(u => u.Transactions).Count() + 1;
        foreach (var transaction in utilisateur.Transactions)
        {
            transaction.Id = transactionId++;
            transaction.UtilisateurId = utilisateur.Id;
            transaction.Utilisateur = utilisateur; // Évitez la référence circulaire
        }

        _utilisateurs.Add(utilisateur);
        return CreatedAtAction(nameof(GetUtilisateur), new { id = utilisateur.Id }, utilisateur);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateUtilisateur(int id, [FromBody] User utilisateur)
    {
        var existingUtilisateur = _utilisateurs.FirstOrDefault(u => u.Id == id);
        if (existingUtilisateur == null)
        {
            return NotFound("Utilisateur non trouvé");
        }

        existingUtilisateur.Nom = utilisateur.Nom;
        // Mettez à jour d'autres champs au besoin

        return NoContent();  // 204 No Content
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUtilisateur(int id)
    {
        var utilisateur = _utilisateurs.FirstOrDefault(u => u.Id == id);
        if (utilisateur == null)
        {
            return NotFound("Utilisateur non trouvé");
        }

        _utilisateurs.Remove(utilisateur);
        return NoContent(); // 204 No Content
    }

    [HttpGet("{id}/transactions")]
    public ActionResult<IEnumerable<Transaction>> GetUserTransactions(int id)
    {
        var utilisateur = _utilisateurs.FirstOrDefault(u => u.Id == id);
        if (utilisateur == null)
        {
            return NotFound("Utilisateur non trouvé");
        }
        return Ok(utilisateur.Transactions);
    }

    [HttpGet("search/{name}")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> SearchUsersByName(string name)
    {
        var users = await _userService.SearchByNameAsync(name);
        if (!users.Any())
        {
            return NotFound("Aucun utilisateur correspondant trouvé.");
        }
        return Ok(users);
    }
}
