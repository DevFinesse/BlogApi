using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IServiceManager
    {
        IPostService PostService { get; }
        ICommentService CommentService { get; }
        ICategoryService CategoryService { get; }
    }
}
