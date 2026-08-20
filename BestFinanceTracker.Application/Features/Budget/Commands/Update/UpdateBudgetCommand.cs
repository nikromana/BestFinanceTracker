using MediatR;

namespace BestFinanceTracker.Application.Features.Budgets.Commands.UpdateBudget;

public record UpdateBudgetCommand(int Id, int CategoryId, int Year, int Month, decimal Limit) : IRequest<bool>;