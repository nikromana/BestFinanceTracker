using BestFinanceTracker.Application.DTOs.Transactions;
using MediatR;

namespace BestFinanceTracker.Application.Features.Transactions.Queries.GetAllTransactions;

public record GetAllTransactionsQuery(int? Year, int? Month) : IRequest<List<TransactionDto>>;