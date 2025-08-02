using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Text.Json;

namespace BlogApi.Presentation.Controllers
{
    [Route("api/posts/{postId}/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CommentsController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentsForPost(Guid postId, [FromQuery] CommentParameters commentParameters)
        {
            var pagedResult = await _service.CommentService.GetCommentsAsync(postId, commentParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.comments);
        }

        [HttpGet("{id:Guid}", Name = "GetCommentForPost")]
        public async Task<IActionResult> GetCommentForPost(Guid postId, Guid id)
        {
            var comment = await _service.CommentService.GetCommentAsync(postId, id, trackChanges: false);
            return Ok(comment);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCommentForPost(Guid postId, [FromBody] CommentCreationDto comment)
        {
            var commentToReturn =await _service.CommentService.CreateCommentForPostAsync(postId, comment, trackChanges: false);
            return Ok(commentToReturn);

        }

        [HttpGet("threaded")]
        public async Task<IActionResult> GetThreadedComments(Guid postId) 
        {
            
            var comments = await _service.CommentService.GetThreadedCommentsAsync(postId, trackChanges: false); 
            return Ok(comments);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCommentForPost(Guid postId, Guid id) 
        {
            await _service.CommentService.DeleteCommentForPostAsync(postId, id, trackChanges: false);
            return NoContent(); 
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCommentForPost(Guid postId, Guid id, [FromBody] CommentUpdateDto comment)
        {
            if (comment is null)
                return BadRequest("body cannot be empty");
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var post = await _service.PostService.GetPostAsync(postId, trackChanges: false);
            await _service.CommentService.UpdateCommentForPostAsync(post.Id, id, comment, postTrackChanges: false, commentTrackChanges: true);
            return NoContent();
            
        }

    }
}
