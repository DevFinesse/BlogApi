using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ICommentService
    {
        IEnumerable<CommentDto> GetComments(Guid postId, bool trackChanges);
        CommentDto GetComment(Guid postId, Guid id, bool trackChanges);

        CommentDto CreateCommentForPost(Guid postId, CommentCreationDto commentCreationDto, bool trackChanges);
        IEnumerable<CommentDto> GetCommentWithReplies (Guid commentId, bool trackChanges);
        IEnumerable<CommentDto> GetThreadedComments(Guid postId, bool trackChanges);
    }
}
