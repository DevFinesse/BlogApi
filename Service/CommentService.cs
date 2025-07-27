using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class CommentService : ICommentService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CommentService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<CommentDto> GetComments(Guid postId, bool trackChanges)
        {
            var post = _repository.PostRepository.GetPost(postId, trackChanges) ?? throw new PostNotFoundException(postId);
            var commentsFromDb = _repository.CommentRepository.GetComments(postId,trackChanges);
            var commentDto = _mapper.Map<IEnumerable<CommentDto>>(commentsFromDb);
            return commentDto;

        }

        public CommentDto GetComment(Guid postId, Guid id, bool trackChanges) 
        {
            var post = _repository.PostRepository.GetPost(postId, trackChanges) ?? throw new PostNotFoundException(postId);
            var commentDb = _repository.CommentRepository.GetComment(postId, id, trackChanges) ?? throw new CommentNotFoundException(id);
            var commentDto = _mapper.Map<CommentDto>(commentDb);
            return commentDto;
        }
    }
}
