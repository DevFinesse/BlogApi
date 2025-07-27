using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class CommentNotFoundException : NotFoundException
    {
        public CommentNotFoundException(Guid commentId) 
            :base($"The comment with the Id of {commentId} does not exist in the database") 
        { }
    }
}
