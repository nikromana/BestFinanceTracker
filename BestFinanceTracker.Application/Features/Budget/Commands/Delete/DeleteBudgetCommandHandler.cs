using BestFinanceTracker.Application.Interfaces;
using MediatR;

namespace BestFinanceTracker.Application.Features.Budgets.Commands.DeleteBudget;

public class DeleteBudgetCommandHandler : IRequestHandler<DeleteBudgetCommand, bool>
{
    private readonly IBudgetRepository _budgetRepository;

    public DeleteBudgetCommandHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    public async Task<bool> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = await _budgetRepository.GetByIdAsync(request.Id);
        if (budget is null) return false;

        await _budgetRepository.DeleteAsync(budget);
        return true;
    }
}