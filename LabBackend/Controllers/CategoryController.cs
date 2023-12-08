using LabBackeend.Controllers;
using LabBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }
        //category/<category_id>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = new CategoryModel { Id = id, Name = $"Category {id}" };
            return Ok(category);
        }
        //category
        [HttpGet]
        public IActionResult Get()
        {
            var categories = Enumerable.Range(1, 5).Select(index => new CategoryModel
            {
                Id = index,
                Name = $"Category {index}"
            });
            return Ok(categories);
        }
        //category
        [HttpPost]
        public IActionResult Post()
        {
            return Ok("New category was added");
        }
        //category/<category_id>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Category {id} was deleated.");
        }
    }
}
