using LabBackeend.Controllers;
using LabBackend.Bussines.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly MemoryCacheService<CategoryModel> _memoryCacheService;
        //private int index = 0;
        public CategoryController(ILogger<CategoryController> logger, MemoryCacheService<CategoryModel> memoryCacheService)
        {
            _logger = logger;
            _memoryCacheService = memoryCacheService;
        }
        //category/<category_id>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var category = _memoryCacheService.GetById(id);
                return Ok(category);
            }
            catch
            {
                return NotFound();
            }
        }
        //category
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var categories = _memoryCacheService.GetAll();
                return Ok(categories);
            }
            catch
            {
                return NotFound();
            }
        }
        //category?name=chlib
        [HttpPost]
        public IActionResult Post([FromQuery] string name)
        {
            try
            {
                var category = new CategoryModel
                {
                    Id = _memoryCacheService.index,
                    Name = name
                };
                _memoryCacheService.Add(category);
                return Ok(category);
            }
            catch
            {
                return BadRequest();
            }
        }
        //category/<category_id>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _memoryCacheService.DeleteById(id);
                return Ok($"Category {id} was deleated.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
