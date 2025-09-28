using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

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

        public async Task<(IEnumerable<CommentDto> comments, MetaData metaData)> GetCommentsAsync(Guid postId, CommentParameters commentParameters, bool trackChanges)
        {
            var post = await _repository.PostRepository.GetPostAsync(postId, trackChanges) ?? throw new PostNotFoundException(postId);
            var commentsWithMetadata = await _repository.CommentRepository.GetCommentsAsync(postId, commentParameters, trackChanges);
            var commentsDto = _mapper.Map<IEnumerable<CommentDto>>(commentsWithMetadata);   
            return (comments: BuildCommentTree(commentsDto), metaData: commentsWithMetadata.MetaData);
        }

        public async Task<CommentDto> GetCommentAsync(Guid postId, Guid id, bool trackChanges) 
        {
            var post = await _repository.PostRepository.GetPostAsync(postId, trackChanges) ?? throw new PostNotFoundException(postId);
            var commentFromDb = await _repository.CommentRepository.GetCommentAsync(postId, id, trackChanges) ?? throw new CommentNotFoundException(id);
            var commentDto = _mapper.Map<CommentDto>(commentFromDb);
            return  commentDto;
        }

        public async Task<CommentDto> CreateCommentForPostAsync(Guid postId, CommentCreationDto commentCreationDto, bool trackChanges)
        {
            var post = await _repository.PostRepository.GetPostAsync(postId, trackChanges) ?? throw new PostNotFoundException(postId);
            
            if (commentCreationDto.ParentCommentId.HasValue)
            {
                var parentComment = await _repository.CommentRepository.GetCommentAsync(postId, commentCreationDto.ParentCommentId.Value, trackChanges) 
                    ?? throw new CommentNotFoundException(commentCreationDto.ParentCommentId.Value);
            }

            var commentEntity = _mapper.Map<Comment>(commentCreationDto);
            _repository.CommentRepository.CreateCommentForPost(postId, commentEntity);
             await _repository.SaveAsync();
            var commentToReturn = _mapper.Map<CommentDto>(commentEntity);
            return commentToReturn;
        }

        public async Task<IEnumerable<CommentDto>> GetThreadedCommentsAsync(Guid postId, bool trackChanges)
        {
            var allComments = await _repository.CommentRepository.GetCommentsAsync(postId, new CommentParameters { PageNumber = 1, PageSize = int.MaxValue }, trackChanges);
            var allCommentDtos = _mapper.Map<IEnumerable<CommentDto>>(allComments);
            
            return BuildCommentTree(allCommentDtos);
        }

        private IEnumerable<CommentDto> BuildCommentTree(IEnumerable<CommentDto> allComments)
        {
            var commentDict = allComments.ToDictionary(c => c.Id);
            var rootComments = new List<CommentDto>();

            foreach (var comment in allComments)
            {
                if (comment.ParentCommentId == null)
                {
                    rootComments.Add(comment);
                }
            }

            return rootComments.Select(rootComment => BuildCommentWithMetrics(rootComment, commentDict, 0));
        }


        private CommentDto BuildCommentWithMetrics(CommentDto comment, Dictionary<Guid, CommentDto> commentDict, int depth)
        {
            var replies = commentDict.Values
                .Where(c => c.ParentCommentId == comment.Id)
                .Select(reply => BuildCommentWithMetrics(reply, commentDict, depth + 1))
                .ToList();

            var totalReplyCount = replies.Count + replies.Sum(r => r.ReplyCount);

            return comment with 
            { 
                Depth = depth,
                ReplyCount = totalReplyCount,
                Replies = replies.Any() ? replies : null
            };
        }

        public async Task DeleteCommentForPostAsync(Guid postId, Guid id, bool trackChanges)
        {
            var post = await _repository.PostRepository.GetPostAsync(postId, trackChanges) ?? throw new PostNotFoundException(postId);
            var commentForPost = await _repository.CommentRepository.GetCommentAsync(postId, id, trackChanges) ?? throw new CommentNotFoundException(id);
            _repository.CommentRepository.DeleteComment(commentForPost);
            await _repository.SaveAsync();

        }

        public async Task UpdateCommentForPostAsync(Guid postId, Guid id, CommentUpdateDto commentUpdate, bool postTrackChanges, bool commentTrackChanges)
        {
            var post = await _repository.PostRepository.GetPostAsync(postId, postTrackChanges) ?? throw new PostNotFoundException(postId);
            var commentEntity = await _repository.CommentRepository.GetCommentAsync(postId, id, commentTrackChanges) ?? throw new CommentNotFoundException(id);
            _mapper.Map(commentUpdate, commentEntity);
            await _repository.SaveAsync();
        }
    }
}
