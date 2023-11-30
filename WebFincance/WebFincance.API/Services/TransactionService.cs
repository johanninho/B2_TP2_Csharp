using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebFincance.API.DTOs;
using WebFincance.API.Models;
using WebFincance.API.Data;

namespace WebFincance.API.Services;


public class TransactionService : ITransactionService
{
    private readonly FinanceContext _context;

    public TransactionService(FinanceContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TransactionDTO>> GetAllAsync()
    {
        var transactions = await _context.Transactions
                                         .Select(t => new TransactionDTO
                                         {
                                             Id = t.Id,
                                             Date = t.Date,
                                             Amount = t.Amount,
                                             Type = t.Type,
                                             UserId = t.UserId
                                         })
                                         .ToListAsync();
        return transactions;
    }

    public async Task<TransactionDTO> GetByIdAsync(int id)
    {
        var transaction = await _context.Transactions
                                        .Where(t => t.Id == id)
                                        .Select(t => new TransactionDTO
                                        {
                                            Id = t.Id,
                                            Date = t.Date,
                                            Amount = t.Amount,
                                            Type = t.Type,
                                            UserId = t.UserId
                                        })
                                        .FirstOrDefaultAsync();
        return transaction;
    }

    public async Task<TransactionDTO> CreateAsync(TransactionDTO transactionDto)
    {
        var transaction = new Transaction
        {
            Date = transactionDto.Date,
            Amount = transactionDto.Amount,
            Type = transactionDto.Type,
            UserId = transactionDto.UserId
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        transactionDto.Id = transaction.Id;
        return transactionDto;
    }

    public async Task<bool> UpdateAsync(TransactionDTO transactionDto)
    {
        var transaction = await _context.Transactions.FindAsync(transactionDto.Id);
        if (transaction == null)
        {
            return false;
        }

        transaction.Date = transactionDto.Date;
        transaction.Amount = transactionDto.Amount;
        transaction.Type = transactionDto.Type;
        // Update other fields as necessary

        _context.Transactions.Update(transaction);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null)
        {
            return false;
        }

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
        return true;
    }
}
