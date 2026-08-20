using BestFinanceTracker.Application.Interfaces;
using MediatR;

namespace BestFinanceTracker.Application.Features.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, bool>
{
    private readonly ITransactionRepository _transactionRepository;

    public UpdateTransactionCommandHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<bool> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetByIdAsync(request.Id);
        if (transaction is null) return false;

        transaction.Amount = request.Amount;
        transaction.Date = request.Date;
        transaction.Type = request.Type;
        transaction.Description = request.Description;
        transaction.CategoryId = request.CategoryId;

        await _transactionRepository.UpdateAsync(transaction);
        return true;
    }
}