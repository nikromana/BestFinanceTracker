using MediatR;

namespace BestFinanceTracker.Application.Features.Budgets.Commands.DeleteBudget;

public record DeleteBudgetCommand(int Id) : IRequest<bool>;