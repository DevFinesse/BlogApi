using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class PostRepository : RepositoryBase<Post> , IPostRepository
    {
        public PostRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<bool> SlugExistsAsync(string slug, Guid? excludedId = null)
        {
            var query = RepositoryContext.Posts.Where(p => p.Slug == slug);
            if (excludedId.HasValue)
            {
                query = query.Where(p => p.Id == excludedId.Value);
            }

            return await query.AnyAsync();
        }
        public async Task<IEnumerable<Post>> GetAllPostsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<Post> GetPostAsync(Guid postId, bool trackChanges)
        {
            return await FindByCondition(p => p.Id.Equals(postId), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<Post> GetPostBySlugAsync(string slug, bool trackChanges)
        {
            return await FindByCondition(p => p.Slug.Equals(slug), trackChanges).SingleOrDefaultAsync();
        }

        
        public void CreatePost(Post post)
        {
            Create(post);
        }

        public async Task<IEnumerable<Post>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            return await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();
        }

        public void DeletePost(Post post)
        {
            Delete(post);
        }

        public async Task<IEnumerable<Post>> GetPostsByCategoryAsync(Guid categoryId, bool trackChanges)
        {
            return await FindByCondition(p => p.CategoryId.Equals(categoryId), trackChanges).OrderByDescending(p => p.CreatedAt).ToListAsync();
        }
    }
}
