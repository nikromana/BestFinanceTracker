using BestFinanceTracker.Domain.Entities;

namespace BestFinanceTracker.Application.Interfaces;

public interface IBudgetRepository
{
    Task<List<Budget>> GetAllAsync(int? year, int? month);
    Task<Budget?> GetByIdAsync(int id);
    Task AddAsync(Budget budget);
    Task UpdateAsync(Budget budget);
    Task DeleteAsync(Budget budget);
}