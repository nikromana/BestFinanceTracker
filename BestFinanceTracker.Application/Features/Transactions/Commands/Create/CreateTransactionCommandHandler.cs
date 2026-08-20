using BestFinanceTracker.Application.DTOs.Transactions;
using BestFinanceTracker.Application.Interfaces;
using BestFinanceTracker.Domain.Entities;
using MediatR;

namespace BestFinanceTracker.Application.Features.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionDto>
{
    private readonly ITransactionRepository _transactionRepository;

    public CreateTransactionCommandHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<TransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = new Transaction
        {
            Amount = request.Amount,
            Date = request.Date,
            Type = request.Type,
            Description = request.Description,
            CategoryId = request.CategoryId
        };

        await _transactionRepository.AddAsync(transaction);

        var saved = await _transactionRepository.GetByIdAsync(transaction.Id);

        return new TransactionDto(saved!.Id, saved.Amount, saved.Date, saved.Type,
            saved.Description, saved.CategoryId, saved.Category.Name);
    }
}