using BestFinanceTracker.Application.DTOs.Budgets;
using MediatR;

namespace BestFinanceTracker.Application.Features.Budgets.Commands.CreateBudget;

public record CreateBudgetCommand(int CategoryId, int Year, int Month, decimal Limit) : IRequest<BudgetDto>;