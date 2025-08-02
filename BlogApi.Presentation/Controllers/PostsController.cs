using BlogApi.Presentation.ModelBinders;
using Microsoft.AspNetCore.JsonPatch;
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
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _service.PostService.GetAllPostsAsync(trackChanges: false);
            return Ok(posts);
        }

        [HttpGet("{id:guid}", Name ="PostById")]
        public async Task<IActionResult> GetPost(Guid id)
        { 
            var post =await _service.PostService.GetPostAsync(id, trackChanges: false);
            return Ok(post);
        }

        [HttpGet("collection/({ids})", Name ="GetPostCollection")]
        public async Task<IActionResult> GetPostCollection([ModelBinder(BinderType =typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            var posts =await _service.PostService.GetByIdsAsync(ids, trackChanges: false);
            return Ok(posts);
        }

        [HttpGet("{slug}", Name ="PostBySlug")]
        public async Task<IActionResult> GetPostBySlug(string slug) 
        { 
            var post = await _service.PostService.GetPostBySlugAsync(slug, trackChanges: false);
            return Ok(post);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreatePost([FromBody] PostCreationDto post)
        {
            var createdPost = await _service.PostService.CreatePostAsync(post);
            var slug = await _service.PostService.GetPostAsync(createdPost.Id, trackChanges: false);
            return CreatedAtAction(nameof(GetPostBySlug), new { slug = slug.Slug }, post);
        }

        [HttpPost("collection")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreatePostCollection([FromBody] IEnumerable<PostCreationDto> postCollection) 
        {
            var result = await _service.PostService.CreatePostCollectionAsync(postCollection);
            return CreatedAtRoute(nameof(GetPostCollection), new { result.ids }, result.posts);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePost(Guid id) 
        { 
            await _service.PostService.DeletePostAsync(id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdatePost(Guid id, [FromBody] PostUpdateDto post)
        {
            await _service.PostService.UpdatePostAsync(id, post, trackChanges: true);
            return NoContent();
        }

        [HttpGet("category")]
        public async Task<IActionResult> GetPostByCategory(Guid categoryId)
        {
            var posts = await _service.PostService.GetPostsByCategoryAsync(categoryId, trackChanges: false);
            return Ok(posts);
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> PartiallyUpdatedPost(Guid id, [FromBody] JsonPatchDocument<PostUpdateDto> patchDoc) 
        {
            if (patchDoc is null)
            {
                return BadRequest("patchDoc is empty");
            }

            var result = await  _service.PostService.GetPostForPatchAsync(id, trackChanges: true);
            patchDoc.ApplyTo(result.postToPatch,ModelState);

            TryValidateModel(result.postToPatch);

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }


            await _service.PostService.SaveChangesForPatchAsync(result.postToPatch, result.postEntity);
            return NoContent();
        }
    }
}
