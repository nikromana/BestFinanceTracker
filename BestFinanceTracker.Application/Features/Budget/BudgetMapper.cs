using BestFinanceTracker.Application.DTOs.Budgets;
using BestFinanceTracker.Domain.Entities;

namespace BestFinanceTracker.Application.Features.Budgets.Common;

public static class BudgetMapper
{
    public static BudgetDto ToDto(Budget budget, decimal spent)
    {
        var remaining = budget.Limit - spent;
        return new BudgetDto(
            budget.Id, budget.CategoryId, budget.Category.Name,
            budget.Year, budget.Month, budget.Limit,
            spent, remaining, spent > budget.Limit);
    }
}