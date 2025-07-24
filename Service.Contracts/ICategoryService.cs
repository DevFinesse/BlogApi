using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAllCategories(bool trackChanges);
    }
}
