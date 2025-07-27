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
        IEnumerable<Post> GetAllPosts(bool trackChanges);

        Post GetPost (Guid postId, bool trackChanges);
        Post GetPostBySlug (string slug, bool trackChanges);
        void CreatePost(Post post);

        Task<bool> SlugExistsAsync(string slug, Guid? excludedId = null);
    }
}
