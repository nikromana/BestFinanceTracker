using Shared;

namespace BestFinanceTracker.Application.DTOs.Transactions;

public record TransactionDto(
    int Id,
    decimal Amount,
    DateOnly Date,
    TransactionType Type,
    string? Description,
    int CategoryId,
    string CategoryName);