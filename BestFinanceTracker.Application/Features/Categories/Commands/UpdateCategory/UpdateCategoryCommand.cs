using MediatR;
using Shared;

namespace BestFinanceTracker.Application.Features.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(int Id, string Name, TransactionType Type) : IRequest<bool>;