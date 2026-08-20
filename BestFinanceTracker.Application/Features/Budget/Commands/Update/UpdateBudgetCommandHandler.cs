using BestFinanceTracker.Application.Interfaces;
using MediatR;

namespace BestFinanceTracker.Application.Features.Budgets.Commands.UpdateBudget;

public class UpdateBudgetCommandHandler : IRequestHandler<UpdateBudgetCommand, bool>
{
    private readonly IBudgetRepository _budgetRepository;

    public UpdateBudgetCommandHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    public async Task<bool> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = await _budgetRepository.GetByIdAsync(request.Id);
        if (budget is null) return false;

        budget.CategoryId = request.CategoryId;
        budget.Year = request.Year;
        budget.Month = request.Month;
        budget.Limit = request.Limit;

        await _budgetRepository.UpdateAsync(budget);
        return true;
    }
}