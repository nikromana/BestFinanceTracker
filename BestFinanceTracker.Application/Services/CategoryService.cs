using BestFinanceTracker.Application.DTOs.Category;
using BestFinanceTracker.Application.Interfaces;
using BestFinanceTracker.Domain.Entities;


namespace BestFinanceTracker.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories.Select(MapToDto).ToList();
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            return category is null ? null : MapToDto(category);
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                TransactionType = dto.TransactionType
            };

            await _categoryRepository.AddAsync(category);

            return MapToDto(category);
        }

        public async Task<bool> UpdateAsync(int id, UpdateCategoryDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category is null)
            { 
                return false;
            }

            category.Name = dto.Name;
            category.TransactionType = dto.TransactionType;

            await _categoryRepository.UpdateAsync(category);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                return false;
            }

            await _categoryRepository.DeleteAsync(category);

            return true;
        }

        private static CategoryDto MapToDto(Category category)
        {
            return new(category.Id, category.Name, category.TransactionType);
        }
    }
}
