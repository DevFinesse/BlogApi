using AutoMapper;
using Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IPostService> _postService;
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<ICommentService> _commentService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger,IMapper mapper)
        {
            _postService = new Lazy<IPostService>(() => new PostService(repositoryManager, logger, mapper));
            _categoryService = new Lazy<ICategoryService>(() => new CategoryService(repositoryManager,logger, mapper));
            _commentService = new Lazy<ICommentService>(() => new CommentService(repositoryManager, logger, mapper));
        }

        public IPostService PostService => _postService.Value;

        public ICommentService CommentService => _commentService.Value;

        public ICategoryService CategoryService => _categoryService.Value;
    }
}
