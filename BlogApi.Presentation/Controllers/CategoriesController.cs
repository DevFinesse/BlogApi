using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace BlogApi.Presentation.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CategoriesController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories() 
        {
                var categories = await _service.CategoryService.GetAllCategoriesAsync(trackChanges: false);
                return Ok(categories);  
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetCategory(Guid id) 
        {
            var category = await _service.CategoryService.GetCategoryAsync(id, trackChanges: false);
            return Ok(category);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCategory(CategoryCreationDto category)
        {
            await _service.CategoryService.CreateCategoryAsync(category, trackChanges: false);
            return Ok(category);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid id) 
        { 
            await _service.CategoryService.DeleteCategoryAsync(id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody]CategoryUpdateDto category)
        {
            await _service.CategoryService.UpdateCategoryAsync(id, category, trackChanges: true);
            return NoContent();
        }
    }
}
