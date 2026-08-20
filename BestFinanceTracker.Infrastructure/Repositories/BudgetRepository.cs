using BestFinanceTracker.Application.Interfaces;
using BestFinanceTracker.Domain.Entities;
using BestFinanceTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BestFinanceTracker.Infrastructure.Repositories;

public class BudgetRepository : IBudgetRepository
{
    private readonly AppDbContext _context;

    public BudgetRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Budget>> GetAllAsync(int? year, int? month)
    {
        var query = _context.Budgets.Include(b => b.Category).AsNoTracking().AsQueryable();

        if (year.HasValue) query = query.Where(b => b.Year == year.Value);
        if (month.HasValue) query = query.Where(b => b.Month == month.Value);

        return await query.ToListAsync();
    }

    public async Task<Budget?> GetByIdAsync(int id) =>
        await _context.Budgets.Include(b => b.Category).FirstOrDefaultAsync(b => b.Id == id);

    public async Task AddAsync(Budget budget)
    {
        _context.Budgets.Add(budget);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Budget budget)
    {
        _context.Budgets.Update(budget);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Budget budget)
    {
        _context.Budgets.Remove(budget);
        await _context.SaveChangesAsync();
    }
}