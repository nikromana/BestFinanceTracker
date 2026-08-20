using MediatR;
using Shared;

namespace BestFinanceTracker.Application.Features.Transactions.Commands.UpdateTransaction;

public record UpdateTransactionCommand(
    int Id,
    decimal Amount,
    DateOnly Date,
    TransactionType Type,
    string? Description,
    int CategoryId) : IRequest<bool>;