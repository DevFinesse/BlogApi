using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace BlogApi.Presentation.Controllers
{
    [Route("api/posts")]
    [ApiController]
    [ApiExplorerSettings(GroupName ="v1")]
    public class PostsController : ControllerBase
    {

        private readonly IServiceManager _service;
        public PostsController(IServiceManager serviceManager)
        {
            _service = serviceManager;
        }

        [HttpGet]
        public IActionResult GetPosts()
        {
            var posts = _service.PostService.GetAllPosts(trackChanges: false);
            return Ok(posts);
        }

        [HttpGet("{id:guid}", Name ="PostById")]
        public IActionResult GetPost(Guid id)
        { 
            var post = _service.PostService.GetPost(id, trackChanges: false);
            return Ok(post);
        }

        [HttpGet("{slug}", Name ="PostBySlug")]
        public IActionResult GetPostBySlug(string slug) 
        { 
            var post = _service.PostService.GetPostBySlug(slug, trackChanges: false);
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostCreationDto post)
        {
            if (post == null)
            return BadRequest("Post cannot be empty");
            var createdPost = await _service.PostService.CreatePostAsync(post);
            var slug = _service.PostService.GetPost(createdPost.Id, trackChanges: false);
            return CreatedAtAction(nameof(GetPostBySlug), new { slug = slug.Slug }, post);
        }
    }
}
