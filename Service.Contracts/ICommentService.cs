using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts
{
    public interface ICommentService
    {
        Task<(IEnumerable<CommentDto> comments, MetaData metaData)> GetCommentsAsync(Guid postId, CommentParameters commentParameters, bool trackChanges);
        Task<CommentDto> GetCommentAsync(Guid postId, Guid id, bool trackChanges);

        Task<CommentDto> CreateCommentForPostAsync(Guid postId, CommentCreationDto commentCreationDto, bool trackChanges);
        Task<IEnumerable<CommentDto>> GetThreadedCommentsAsync(Guid postId, bool trackChanges);
        Task DeleteCommentForPostAsync(Guid postId,Guid id, bool trackChanges);
        Task UpdateCommentForPostAsync(Guid postId, Guid id, CommentUpdateDto commentUpdate,
            bool postTrackChanges, bool commentTrackChanges);
    }
}
