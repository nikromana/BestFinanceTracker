using BestFinanceTracker.Application.DTOs.Budgets;

namespace BestFinanceTracker.Application.DTOs.Summary;

public record MonthlySummaryDto(
    int Year,
    int Month,
    decimal TotalIncome,
    decimal TotalExpense,
    decimal Balance,
    List<CategoryBreakdownDto> CategoryBreakdown,
    List<BudgetDto> BudgetComparisons,
    CategoryBreakdownDto? TopExpenseCategory);