using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAllCategories(bool trackChanges);
        CategoryDto GetCategory(Guid categoryId, bool trackChanges);
    }
}
