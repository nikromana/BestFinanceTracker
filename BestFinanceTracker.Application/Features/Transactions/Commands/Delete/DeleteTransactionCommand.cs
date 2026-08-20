using MediatR;

namespace BestFinanceTracker.Application.Features.Transactions.Commands.DeleteTransaction;

public record DeleteTransactionCommand(int Id) : IRequest<bool>;