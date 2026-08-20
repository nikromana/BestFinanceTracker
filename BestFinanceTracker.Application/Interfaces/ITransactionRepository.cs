using BestFinanceTracker.Domain.Entities;

namespace BestFinanceTracker.Application.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetAllAsync(int? year, int? month);
        Task<Transaction?> GetByIdAsync(int id);
        Task AddAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(Transaction transaction);
    }
}
