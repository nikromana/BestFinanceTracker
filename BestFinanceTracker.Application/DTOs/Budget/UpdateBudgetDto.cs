namespace BestFinanceTracker.Application.DTOs.Budgets;

public record UpdateBudgetDto(int CategoryId, int Year, int Month, decimal Limit);