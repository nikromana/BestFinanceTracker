using BestFinanceTracker.Application.DTOs.Transactions;
using BestFinanceTracker.Application.Interfaces;
using MediatR;

namespace BestFinanceTracker.Application.Features.Transactions.Queries.GetTransactionById;

public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto?>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionByIdQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<TransactionDto?> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetByIdAsync(request.Id);

        return transaction is null ? null : 
            new TransactionDto(transaction.Id,
            transaction.Amount, 
            transaction.Date, 
            transaction.Type,
            transaction.Description, 
            transaction.CategoryId, 
            transaction.Category.Name);
    }
}