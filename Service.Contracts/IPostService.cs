using Entities.LinkModels;
using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Dynamic;

namespace Service.Contracts
{
    public interface IPostService
    {
        Task<(LinkResponse linkResponse, MetaData metaData)> GetAllPostsAsync(bool trackChanges, LinkParameters linkParameters);

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
