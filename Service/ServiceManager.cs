using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IPostService> _postService;
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<ICommentService> _commentService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(IRepositoryManager repositoryManager,
            ILoggerManager logger,IMapper mapper, ISlugService slugService, 
            IPostLinks postLinks, UserManager<User> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _postService = new Lazy<IPostService>(() => new PostService(repositoryManager, logger, mapper, slugService, postLinks));
            _categoryService = new Lazy<ICategoryService>(() => new CategoryService(repositoryManager,logger, mapper));
            _commentService = new Lazy<ICommentService>(() => new CommentService(repositoryManager, logger, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(logger, mapper,userManager,configuration, roleManager));
        }

        public IPostService PostService => _postService.Value;

        public ICommentService CommentService => _commentService.Value;

        public ICategoryService CategoryService => _categoryService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
