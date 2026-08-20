namespace BestFinanceTracker.Application.DTOs.Budgets;

public record CreateBudgetDto(int CategoryId, int Year, int Month, decimal Limit);