using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Comment> GetComments(Guid postId, bool trackChanges)
        {
            return FindByCondition(c => c.PostId.Equals(postId),trackChanges)
                .Include(c => c.Replies)
                .OrderBy(c => c.CreatedAt).ToList();
        }

        public Comment GetComment(Guid postId, Guid id, bool trackChanges) 
        {
            return FindByCondition(c => c.PostId.Equals(postId) && c.Id.Equals(id), trackChanges).Include(c => c.Replies).SingleOrDefault();
        }

        public void CreateCommentForPost(Guid postId, Comment comment) 
        { 
            comment.PostId = postId;
            Create(comment);
        }

        public IEnumerable<Comment> GetCommentWithReplies(Guid commentId, bool trackChanges)
        {
           return FindByCondition(c => c.ParentCommentId.Equals(commentId),trackChanges).Include(c => c.Replies).OrderByDescending(c => c.CreatedAt).ToList();
         
        }

        public IEnumerable<Comment> GetThreadedComments(Guid postId, bool trackChanges)
        { 
            // Start with root comments (no parent)
            var query = FindByCondition(c => c.PostId.Equals(postId) && c.ParentCommentId == null, trackChanges);
            
            // Recursively include all nested replies
            var comments = LoadRepliesRecursively(query);
            
            return comments.OrderByDescending(c => c.CreatedAt).ToList();
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
    }
}
