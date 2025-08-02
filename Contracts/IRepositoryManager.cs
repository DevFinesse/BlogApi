using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IPostRepository PostRepository { get; }
        ICommentRepository CommentRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        Task SaveAsync();
    }
}
