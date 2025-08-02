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
                .ToListAsync();

            return PagedList<Comment>
                .ToPagedList(comments, commentParameters.PageNumber, commentParameters.PageSize);
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
            // Start with root comments (no parent)
            var query = FindByCondition(c => c.PostId.Equals(postId) && c.ParentCommentId == null, trackChanges);
            
            // Recursively include all nested replies
            var comments = LoadRepliesRecursively(query);
            
            return await comments.OrderByDescending(c => c.CreatedAt).ToListAsync();
        }

        private IQueryable<Comment> LoadRepliesRecursively(IQueryable<Comment> query)
        {
            // Load immediate replies
            query = query.Include(c => c.Replies);
            
            // Get all replies
            var replies = query.SelectMany(c => c.Replies);
            
            // If there are replies, recursively load their replies too
            if (replies.Any())
            {
                var replyQuery = RepositoryContext.Comments
                    .Where(c => replies.Select(r => r.Id).Contains(c.Id));
                    
                LoadRepliesRecursively(replyQuery);
            }
            
            return query;
        }

        public void DeleteComment(Comment comment)
        {
            Delete(comment);
        }
    }
}
