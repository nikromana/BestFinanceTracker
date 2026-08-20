namespace BestFinanceTracker.Application.DTOs.Budgets;

public record BudgetDto(
    int Id,
    int CategoryId,
    string CategoryName,
    int Year,
    int Month,
    decimal Limit,
    decimal Spent,
    decimal Remaining,
    bool IsOverBudget);