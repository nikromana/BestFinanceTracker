using BestFinanceTracker.Application.DTOs.Transactions;
using MediatR;
using Shared;

namespace BestFinanceTracker.Application.Features.Transactions.Commands.CreateTransaction;

public record CreateTransactionCommand(
    decimal Amount,
    DateOnly Date,
    TransactionType Type,
    string? Description,
    int CategoryId) : IRequest<TransactionDto>;