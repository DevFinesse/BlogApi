using Entities.Models;
using Contracts;

namespace Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public IEnumerable<Category> GetAllCategories(bool trackChanges)
        {
            return FindAll(trackChanges).OrderBy(c => c.Name).ToList();
        }

        public Category GetCategory(Guid categoryId, bool trackChanges)
        {
            return FindByCondition(c => c.Id.Equals(categoryId), trackChanges).SingleOrDefault();
        }
    }
}
