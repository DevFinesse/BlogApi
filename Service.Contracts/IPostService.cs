using Entities.Models;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetAllPostsAsync(bool trackChanges);

        Task<PostDto> GetPostAsync(Guid id, bool trackChanges);
        Task<IEnumerable<PostDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        Task<PostDto> GetPostBySlugAsync(string slug, bool trackChanges);
        Task<PostDto> CreatePostAsync(PostCreationDto post);
        Task<(IEnumerable<PostDto> posts, string ids)> CreatePostCollectionAsync (IEnumerable<PostCreationDto> postCollection);
        Task DeletePostAsync(Guid postId, bool trackChanges);
        Task UpdatePostAsync(Guid postId, PostUpdateDto postUpdate, bool trackChanges);
        Task<IEnumerable<PostDto>> GetPostsByCategoryAsync(Guid categoryId, bool trackChanges);
        Task<(PostUpdateDto postToPatch, Post postEntity)> GetPostForPatchAsync(Guid postId,  bool trackChanges);
        Task SaveChangesForPatchAsync(PostUpdateDto postToPatch, Post postEntity);
    }
}
