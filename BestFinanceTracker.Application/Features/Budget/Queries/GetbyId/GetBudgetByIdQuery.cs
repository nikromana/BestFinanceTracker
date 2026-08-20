using BestFinanceTracker.Application.DTOs.Budgets;
using MediatR;

namespace BestFinanceTracker.Application.Features.Budgets.Queries.GetBudgetById;

public record GetBudgetByIdQuery(int Id) : IRequest<BudgetDto?>;