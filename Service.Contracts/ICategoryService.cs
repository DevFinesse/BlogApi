using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync (bool trackChanges);
        Task<CategoryDto> GetCategoryAsync (Guid categoryId, bool trackChanges);
        Task<CategoryDto> CreateCategoryAsync (CategoryCreationDto category, bool trackChanges);
        Task DeleteCategoryAsync (Guid categoryId, bool trackChanges);
        Task UpdateCategoryAsync (Guid categoryId, CategoryUpdateDto category, bool trackChanges);
    }
}
