using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetComments(Guid postId, bool trackChanges);
        Comment GetComment(Guid postId, Guid id, bool trackChanges);
        void CreateCommentForPost(Guid postId, Comment comment);
        IEnumerable<Comment> GetCommentWithReplies(Guid commentId, bool trackChanges);
        IEnumerable<Comment> GetThreadedComments(Guid postId, bool trackChanges);
    }
}
