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
        public IEnumerable<Post> GetAllPosts(bool trackChanges)
        {
            return FindAll(trackChanges)
                .OrderBy(p => p.CreatedAt)
                .ToList();
        }

        public Post GetPost(Guid postId, bool trackChanges)
        {
            return FindByCondition(p => p.Id.Equals(postId), trackChanges).SingleOrDefault();
        }

        public Post GetPostBySlug(string slug, bool trackChanges)
        {
            return FindByCondition(p => p.Slug.Equals(slug), trackChanges).SingleOrDefault();
        }
        public void CreatePost(Post post)
        {
            Create(post);
        }
    }
}
