using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IPostService
    {
        IEnumerable<PostDto> GetAllPosts(bool trackChanges);

        PostDto GetPost(Guid id, bool trackChanges);

        PostDto GetPostBySlug(string slug, bool trackChanges);
        Task<PostDto> CreatePostAsync(PostCreationDto post);
    }
}
