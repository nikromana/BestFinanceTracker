using BestFinanceTracker.Application.DTOs.Budgets;
using BestFinanceTracker.Application.Features.Budgets.Common;
using BestFinanceTracker.Application.Interfaces;
using BestFinanceTracker.Domain.Entities;
using MediatR;

namespace BestFinanceTracker.Application.Features.Budgets.Commands.CreateBudget;

public class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, BudgetDto>
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly ITransactionRepository _transactionRepository;

    public CreateBudgetCommandHandler(IBudgetRepository budgetRepository, ITransactionRepository transactionRepository)
    {
        _budgetRepository = budgetRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<BudgetDto> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = new Budget
        {
            CategoryId = request.CategoryId,
            Year = request.Year,
            Month = request.Month,
            Limit = request.Limit
        };

        await _budgetRepository.AddAsync(budget);

        var saved = await _budgetRepository.GetByIdAsync(budget.Id);
        var spent = await BudgetSpentCalculator.CalculateAsync(_transactionRepository, saved!.CategoryId, saved.Year, saved.Month);

        return BudgetMapper.ToDto(saved, spent);
    }
}