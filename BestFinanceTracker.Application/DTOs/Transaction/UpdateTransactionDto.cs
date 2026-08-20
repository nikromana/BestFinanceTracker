using Shared;

namespace BestFinanceTracker.Application.DTOs.Transactions;

public record UpdateTransactionDto(
    decimal Amount,
    DateOnly Date,
    TransactionType Type,
    string? Description,
    int CategoryId);