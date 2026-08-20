using BestFinanceTracker.Application.DTOs.Category;
using BestFinanceTracker.Application.Interfaces;
using BestFinanceTracker.Domain.Entities;
using MediatR;

namespace BestFinanceTracker.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category { Name = request.Name,  TransactionType = request.Type };

        await _categoryRepository.AddAsync(category);

        return new CategoryDto(category.Id, category.Name, category.TransactionType);
    }
}