using BestFinanceTracker.Application.DTOs.Budgets;
using MediatR;

namespace BestFinanceTracker.Application.Features.Budgets.Queries.GetAllBudgets;

public record GetAllBudgetsQuery(int? Year, int? Month) : IRequest<List<BudgetDto>>;