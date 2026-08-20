using BestFinanceTracker.Application.DTOs.Budgets;
using BestFinanceTracker.Application.Features.Budgets.Common;
using BestFinanceTracker.Application.Interfaces;
using MediatR;

namespace BestFinanceTracker.Application.Features.Budgets.Queries.GetAllBudgets;

public class GetAllBudgetsQueryHandler : IRequestHandler<GetAllBudgetsQuery, List<BudgetDto>>
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly ITransactionRepository _transactionRepository;

    public GetAllBudgetsQueryHandler(IBudgetRepository budgetRepository, ITransactionRepository transactionRepository)
    {
        _budgetRepository = budgetRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<List<BudgetDto>> Handle(GetAllBudgetsQuery request, CancellationToken cancellationToken)
    {
        var budgets = await _budgetRepository.GetAllAsync(request.Year, request.Month);
        var result = new List<BudgetDto>();

        foreach (var budget in budgets)
        {
            var spent = await BudgetSpentCalculator.CalculateAsync(_transactionRepository, budget.CategoryId, budget.Year, budget.Month);
            result.Add(BudgetMapper.ToDto(budget, spent));
        }

        return result;
    }
}