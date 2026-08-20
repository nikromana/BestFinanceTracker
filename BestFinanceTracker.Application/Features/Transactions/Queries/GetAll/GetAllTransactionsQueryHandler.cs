using BestFinanceTracker.Application.DTOs.Transactions;
using BestFinanceTracker.Application.Interfaces;
using MediatR;

namespace BestFinanceTracker.Application.Features.Transactions.Queries.GetAllTransactions;

public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, List<TransactionDto>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetAllTransactionsQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<List<TransactionDto>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
    {
        var transactions = await _transactionRepository.GetAllAsync(request.Year, request.Month);

        return transactions.Select(t => new TransactionDto(
            t.Id, t.Amount, t.Date, t.Type, t.Description, t.CategoryId, t.Category.Name)).ToList();
    }
}