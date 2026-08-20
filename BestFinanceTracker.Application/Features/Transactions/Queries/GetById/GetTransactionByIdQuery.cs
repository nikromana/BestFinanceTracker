using BestFinanceTracker.Application.DTOs.Transactions;
using MediatR;

namespace BestFinanceTracker.Application.Features.Transactions.Queries.GetTransactionById;

public record GetTransactionByIdQuery(int Id) : IRequest<TransactionDto?>;