using BestFinanceTracker.Application.DTOs.Summary;
using MediatR;

namespace BestFinanceTracker.Application.Features.Summary.Queries.GetMonthlySummary;

public record GetMonthlySummaryQuery(int Year, int Month) : IRequest<MonthlySummaryDto>;