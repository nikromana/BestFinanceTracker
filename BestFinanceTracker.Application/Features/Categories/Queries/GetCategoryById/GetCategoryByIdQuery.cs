using BestFinanceTracker.Application.DTOs.Category;
using MediatR;

namespace BestFinanceTracker.Application.Features.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(int Id) : IRequest<CategoryDto?>;