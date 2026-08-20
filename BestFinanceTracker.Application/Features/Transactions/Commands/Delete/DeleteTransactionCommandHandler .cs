using BestFinanceTracker.Application.Interfaces;
using MediatR;

namespace BestFinanceTracker.Application.Features.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, bool>
{
    private readonly ITransactionRepository _transactionRepository;

    public DeleteTransactionCommandHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<bool> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetByIdAsync(request.Id);
        if (transaction is null) return false;

        await _transactionRepository.DeleteAsync(transaction);
        return true;
    }
}