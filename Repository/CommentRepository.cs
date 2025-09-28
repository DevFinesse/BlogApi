using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

namespace Repository
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<Comment>> GetCommentsAsync(Guid postId, CommentParameters commentParameters, bool trackChanges)
        {
            var comments =  await FindByCondition(c => c.PostId.Equals(postId),trackChanges)
                .Include(c => c.Replies)
                .OrderBy(c => c.CreatedAt)
                .Skip((commentParameters.PageNumber - 1) * commentParameters.PageSize )
                .Take(commentParameters.PageSize)
                .ToListAsync();

            var count = await FindByCondition(c => c.PostId.Equals(postId), trackChanges).CountAsync();

            return new PagedList<Comment>(comments, count, commentParameters.PageNumber, commentParameters.PageSize);
        }

        public async Task<Comment> GetCommentAsync(Guid postId,  Guid id, bool trackChanges) 
        {
            return await FindByCondition(c => c.PostId.Equals(postId) && c.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        }

        public void CreateCommentForPost(Guid postId, Comment comment) 
        { 
            comment.PostId = postId;
            Create(comment);
        }

        public async Task<IEnumerable<Comment>> GetThreadedCommentsAsync(Guid postId, bool trackChanges)
        { 
            var allComments = await FindByCondition(c => c.PostId.Equals(postId), trackChanges)
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();

            return allComments.Where(c => c.ParentCommentId == null);
        }

        public void DeleteComment(Comment comment)
        {
            Delete(comment);
        }
    }
}
