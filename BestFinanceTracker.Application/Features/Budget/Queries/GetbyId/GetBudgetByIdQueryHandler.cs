using BestFinanceTracker.Application.DTOs.Budgets;
using BestFinanceTracker.Application.Features.Budgets.Common;
using BestFinanceTracker.Application.Interfaces;
using MediatR;

namespace BestFinanceTracker.Application.Features.Budgets.Queries.GetBudgetById;

public class GetBudgetByIdQueryHandler : IRequestHandler<GetBudgetByIdQuery, BudgetDto?>
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly ITransactionRepository _transactionRepository;

    public GetBudgetByIdQueryHandler(IBudgetRepository budgetRepository, ITransactionRepository transactionRepository)
    {
        _budgetRepository = budgetRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<BudgetDto?> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken)
    {
        var budget = await _budgetRepository.GetByIdAsync(request.Id);

        if (budget is null) return null;

        var spent = await BudgetSpentCalculator.CalculateAsync(_transactionRepository, budget.CategoryId, budget.Year, budget.Month);
        
        return BudgetMapper.ToDto(budget, spent);
    }
}