using BestFinanceTracker.Application.Interfaces;
using BestFinanceTracker.Domain.Entities;
using BestFinanceTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BestFinanceTracker.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Transaction>> GetAllAsync(int? year, int? month)
    {
        var query = _context.Transactions.Include(t => t.Category).AsNoTracking().AsQueryable();

        if (year.HasValue)
            query = query.Where(t => t.Date.Year == year.Value);

        if (month.HasValue)
            query = query.Where(t => t.Date.Month == month.Value);

        return await query.OrderByDescending(t => t.Date).ToListAsync();
    }

    public async Task<Transaction?> GetByIdAsync(int id) =>
        await _context.Transactions.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == id);

    public async Task AddAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Transaction transaction)
    {
        _context.Transactions.Update(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Transaction transaction)
    {
        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
    }
}