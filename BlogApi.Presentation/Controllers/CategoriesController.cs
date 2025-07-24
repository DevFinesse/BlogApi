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
                throw new Exception("Exception");
                var categories = _service.CategoryService.GetAllCategories(trackChanges: false);
                return Ok(categories);  
        }
    }
}
