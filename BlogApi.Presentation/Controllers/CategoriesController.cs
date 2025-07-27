using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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
        public IActionResult GetCategories() 
        {
                var categories = _service.CategoryService.GetAllCategories(trackChanges: false);
                return Ok(categories);  
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetCategory(Guid id) 
        {
            var category = _service.CategoryService.GetCategory(id, trackChanges: false);
            return Ok(category);
        }
    }
}
