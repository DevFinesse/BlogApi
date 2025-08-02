using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface ICommentRepository
    {
        Task<PagedList<Comment>> GetCommentsAsync(Guid postId, CommentParameters commentParameters, bool trackChanges);
        Task<Comment> GetCommentAsync(Guid postId, Guid id, bool trackChanges);
        void CreateCommentForPost(Guid postId, Comment comment);
        Task<IEnumerable<Comment>> GetThreadedCommentsAsync(Guid postId, bool trackChanges);
        void DeleteComment(Comment comment); 
    }
}
