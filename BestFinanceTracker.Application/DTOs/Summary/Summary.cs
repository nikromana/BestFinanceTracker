using Shared;

namespace BestFinanceTracker.Application.DTOs.Summary;

public record CategoryBreakdownDto(int CategoryId, string CategoryName, TransactionType Type, decimal Total);