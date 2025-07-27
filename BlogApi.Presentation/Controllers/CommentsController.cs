using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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
        public IActionResult GetCommentsForPost(Guid postId) 
        {
            var comments = _service.CommentService.GetComments(postId, trackChanges: false);
            return Ok(comments);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetCommentForPost(Guid postId, Guid id) 
        {
            var comment = _service.CommentService.GetComment(postId, id, trackChanges: false);
            return Ok(comment);
        }
    }
}
