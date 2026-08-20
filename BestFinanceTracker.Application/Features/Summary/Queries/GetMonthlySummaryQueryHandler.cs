using BestFinanceTracker.Application.DTOs.Summary;
using BestFinanceTracker.Application.Features.Budgets.Common;
using BestFinanceTracker.Application.Interfaces;
using MediatR;
using Shared;

namespace BestFinanceTracker.Application.Features.Summary.Queries.GetMonthlySummary;

public class GetMonthlySummaryQueryHandler : IRequestHandler<GetMonthlySummaryQuery, MonthlySummaryDto>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IBudgetRepository _budgetRepository;

    public GetMonthlySummaryQueryHandler(ITransactionRepository transactionRepository, IBudgetRepository budgetRepository)
    {
        _transactionRepository = transactionRepository;
        _budgetRepository = budgetRepository;
    }

    public async Task<MonthlySummaryDto> Handle(GetMonthlySummaryQuery request, CancellationToken cancellationToken)
    {
        var transactions = await _transactionRepository.GetAllAsync(request.Year, request.Month);
        var budgets = await _budgetRepository.GetAllAsync(request.Year, request.Month);

        var totalIncome = transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
        var totalExpense = transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);

        var categoryBreakdown = transactions
            .GroupBy(t => new { t.CategoryId, CategoryName = t.Category.Name, t.Type })
            .Select(g => new CategoryBreakdownDto(g.Key.CategoryId, g.Key.CategoryName, g.Key.Type, g.Sum(t => t.Amount)))
            .OrderByDescending(c => c.Total)
            .ToList();

        var topExpenseCategory = categoryBreakdown
            .Where(c => c.Type == TransactionType.Expense)
            .OrderByDescending(c => c.Total)
            .FirstOrDefault();

        var budgetComparisons = budgets.Select(budget =>
        {
            var spent = transactions
                .Where(t => t.CategoryId == budget.CategoryId && t.Type == TransactionType.Expense)
                .Sum(t => t.Amount);
            return BudgetMapper.ToDto(budget, spent);
        }).ToList();

        return new MonthlySummaryDto(
            request.Year, request.Month, totalIncome, totalExpense, totalIncome - totalExpense,
            categoryBreakdown, budgetComparisons, topExpenseCategory);
    }
}