using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

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

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync(bool trackChanges)
        {
            var posts = await _repository.PostRepository.GetAllPostsAsync(trackChanges);
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            return postsDto;
        }

        public async Task<PostDto> GetPostAsync(Guid id, bool trackChanges)
        {
            var post =  await _repository.PostRepository.GetPostAsync(id, trackChanges);
            var postDto = _mapper.Map<PostDto>(post);
            return postDto;
        }

        public async Task<PostDto> GetPostBySlugAsync(string slug, bool trackChanges)
        {
            var post = await _repository.PostRepository.GetPostBySlugAsync(slug, trackChanges);
            var postDto = _mapper.Map<PostDto>(post);
            return postDto;
        }

        public async Task<PostDto> CreatePostAsync(PostCreationDto post)
        {
            var postEntity = _mapper.Map<Post>(post);
            postEntity.Slug = await _slugService.GenerateUniqueSlug(post.Title, async s => await _repository.PostRepository.SlugExistsAsync(s));
            _repository.PostRepository.CreatePost(postEntity);
            await _repository.SaveAsync();

            var postToReturn = _mapper.Map<PostDto>(postEntity);
            return postToReturn;
        }

        public async Task<IEnumerable<PostDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
            {
                throw new IdParametersBadRequestExceptions();
            }

            var postEntities = await _repository.PostRepository.GetByIdsAsync(ids, trackChanges);
            if (ids.Count() != postEntities.Count())
            {
                throw new CollectionByIdsBadRequestException();
            }

            var companiesToReturn = _mapper.Map<IEnumerable<PostDto>>(postEntities);
            return companiesToReturn;
        }

        public async  Task<(IEnumerable<PostDto> posts, string ids)> CreatePostCollectionAsync(IEnumerable<PostCreationDto> postCollection)
        {
            if (postCollection is null)
            {
                throw new PostCollectionBadRequest();
            }

            var postEntities = _mapper.Map<IEnumerable<Post>>(postCollection);
            foreach(var post in postEntities)
            {
                post.Slug = await _slugService.GenerateUniqueSlug(post.Title, async s => await _repository.PostRepository.SlugExistsAsync(s));
                _repository.PostRepository.CreatePost(post);
            }

            await _repository.SaveAsync();

            var postCollectionToReturn = _mapper.Map<IEnumerable<PostDto>>(postEntities);
            var ids = string.Join(",", postCollectionToReturn.Select(p => p.Id));

            return (posts: postCollectionToReturn, ids);
        }

        public async Task DeletePostAsync(Guid postId, bool trackChanges)
        {
            var post = await  _repository.PostRepository.GetPostAsync(postId, trackChanges) ?? throw new PostNotFoundException(postId);
            _repository.PostRepository.DeletePost(post);
            await _repository.SaveAsync();
        }

        public async Task UpdatePostAsync(Guid postId, PostUpdateDto postUpdate, bool trackChanges)
        {
            var postEntity = await  _repository.PostRepository.GetPostAsync (postId, trackChanges) ?? throw new PostNotFoundException(postId);
            _mapper.Map(postUpdate, postEntity);
            // postEntity.Slug = await _slugService.GenerateUniqueSlug(postUpdate.Title, async s => await _repository.PostRepository.SlugExistsAsync(s));
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<PostDto>> GetPostsByCategoryAsync(Guid categoryId, bool trackChanges)
        {
            var posts = await _repository.PostRepository.GetPostsByCategoryAsync(categoryId, trackChanges);
            var postsToReturn = _mapper.Map<IEnumerable<PostDto>>(posts);
            return postsToReturn;
        }

        public async Task<(PostUpdateDto postToPatch, Post postEntity)> GetPostForPatchAsync(Guid postId, bool trackChanges)
        {
            var postEntity = await _repository.PostRepository.GetPostAsync(postId, trackChanges);
            var postToPatch = _mapper.Map<PostUpdateDto>(postEntity);
            return (postToPatch, postEntity);
        }

        public async Task SaveChangesForPatchAsync(PostUpdateDto postToPatch, Post postEntity)
        {
            _mapper.Map(postToPatch, postEntity);
            await _repository.SaveAsync();
        }
    }
}
