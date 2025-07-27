using AutoMapper;
using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class PostService : IPostService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly ISlugService _slugService;

        public PostService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, ISlugService slugService)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _slugService = slugService;
        }

        public IEnumerable<PostDto> GetAllPosts(bool trackChanges)
        {
            var posts = _repository.PostRepository.GetAllPosts(trackChanges);
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            return postsDto;
        }

        public PostDto GetPost(Guid id, bool trackChanges)
        {
            var post = _repository.PostRepository.GetPost(id, trackChanges);
            var postDto = _mapper.Map<PostDto>(post);
            return postDto;
        }

        public PostDto GetPostBySlug(string slug, bool trackChanges)
        {
            var post = _repository.PostRepository.GetPostBySlug(slug, trackChanges);
            var postDto = _mapper.Map<PostDto>(post);
            return postDto;
        }

        public async Task<PostDto> CreatePostAsync(PostCreationDto post)
        {
            var postEntity = _mapper.Map<Post>(post);
            postEntity.Slug = await _slugService.GenerateUniqueSlug(post.Title, async s => await _repository.PostRepository.SlugExistsAsync(s));
             _repository.PostRepository.CreatePost(postEntity);
             _repository.Save();

            var postToReturn = _mapper.Map<PostDto>(postEntity);
            return postToReturn;
        }
    }
}
