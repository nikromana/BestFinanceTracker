using BestFinanceTracker.Application.DTOs.Category;
using MediatR;
using Shared;

namespace BestFinanceTracker.Application.Features.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name, TransactionType Type) : IRequest<CategoryDto>;