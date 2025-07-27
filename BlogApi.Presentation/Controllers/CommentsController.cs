using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace BlogApi.Presentation.Controllers
{
    [Route("api/posts/{slug}")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CommentsController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet("comments")]
        public IActionResult GetCommentsForPost(string slug)
        {
            var post = _service.PostService.GetPostBySlug(slug, trackChanges: false);
            var comments = _service.CommentService.GetComments(post.Id, trackChanges: false);
            return Ok(comments);
        }

        [HttpGet("comment/{id:Guid}", Name = "GetCommentForPost")]
        public IActionResult GetCommentForPost(string slug, Guid id)
        {
            var post = _service.PostService.GetPostBySlug(slug, trackChanges: false);
            var comment = _service.CommentService.GetComment(post.Id, id, trackChanges: false);
            return Ok(comment);
        }

        [HttpPost("comment")]
        public IActionResult CreateCommentForPost(string slug, [FromBody] CommentCreationDto comment)
        {
            var post = _service.PostService.GetPostBySlug(slug, trackChanges: false);
            if (post is null)
            {
                return BadRequest("Post does not exist");
            }
            if (comment is null)
                return BadRequest("CommentForCreationDto Is null");

            var commentToReturn = _service.CommentService.CreateCommentForPost(post.Id, comment, trackChanges: false);
            return Ok(commentToReturn);

        }

        [HttpGet("comments/{commentId}")]
        public IActionResult GetCommentWithReplies(string slug, Guid commentId)
        {
            var post = _service.PostService.GetPostBySlug(slug, trackChanges: false);
            var comments = _service.CommentService.GetCommentWithReplies(commentId, trackChanges: false);
            return Ok(comments);
        }

        [HttpGet("comments/threaded")]
        public IActionResult GetThreadedComments(string slug) 
        {
            var post = _service.PostService.GetPostBySlug(slug, trackChanges: false);
            var comments = _service.CommentService.GetThreadedComments(post.Id, trackChanges: false); 
            return Ok(comments);
        }
    }
}
