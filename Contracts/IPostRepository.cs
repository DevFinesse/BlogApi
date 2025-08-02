using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync(bool trackChanges);
        Task<Post> GetPostAsync (Guid postId, bool trackChanges);
        Task<IEnumerable<Post>> GetByIdsAsync (IEnumerable<Guid> ids, bool trackChanges);
        Task<Post> GetPostBySlugAsync (string slug, bool trackChanges);
        void CreatePost(Post post);
        void DeletePost(Post post);
        Task<bool> SlugExistsAsync(string slug, Guid? excludedId = null);

        Task<IEnumerable<Post>> GetPostsByCategoryAsync (Guid categoryId, bool trackChanges);
    }
}
