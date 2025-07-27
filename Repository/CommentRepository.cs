using Contracts;
using Entities.Models;

namespace Repository
{
    internal class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Comment> GetComments(Guid postId, bool trackChanges)
        {
            return FindByCondition(c => c.PostId.Equals(postId),trackChanges)
                .OrderBy(c => c.CreatedAt).ToList();
        }

        public Comment GetComment(Guid postId, Guid id, bool trackChanges) 
        {
            return FindByCondition(c => c.PostId.Equals(postId) && c.Id.Equals(id), trackChanges).SingleOrDefault();
        }
    }
}
