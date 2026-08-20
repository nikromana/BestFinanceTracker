using BestFinanceTracker.Application.DTOs.Category;
using MediatR;

namespace BestFinanceTracker.Application.Features.Categories.Queries.GetAllCategories;

public record GetAllCategoriesQuery : IRequest<List<CategoryDto>>;