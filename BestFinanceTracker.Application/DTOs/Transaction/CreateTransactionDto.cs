using Shared;

namespace BestFinanceTracker.Application.DTOs.Transactions;

public record CreateTransactionDto(
    decimal Amount,
    DateOnly Date,
    TransactionType Type,
    string? Description,
    int CategoryId);