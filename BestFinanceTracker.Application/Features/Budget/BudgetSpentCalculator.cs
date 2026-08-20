using BestFinanceTracker.Application.Interfaces;
using Shared;

namespace BestFinanceTracker.Application.Features.Budgets.Common;

public static class BudgetSpentCalculator
{
    public static async Task<decimal> CalculateAsync(
        ITransactionRepository transactionRepository, int categoryId, int year, int month)
    {
        var transactions = await transactionRepository.GetAllAsync(year, month, categoryId);

        return transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
    }
}