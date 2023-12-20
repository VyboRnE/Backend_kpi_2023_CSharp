using LabBackeend.Controllers;
using LabBackend.Business.Interfaces;
using LabBackend.Business.Models;
using LabBackend.Business.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        //category/<category_id>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> GetById(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                return Ok(category);
            }
            catch (ShopException ex)
            {
                return NotFound(ex.Message);
            }
        }
        //category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> Get()
        {
            try
            {
                var categories = await _categoryService.GetAllAsync();
                return Ok(categories);
            }
            catch (ShopException ex)
            {
                return NotFound(ex.Message);
            }
        }
        //category
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryModel model)
        {
            try
            {
                await _categoryService.AddAsync(model);
                return Ok(model);
            }
            catch (ShopException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //category/<category_id>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteByIdAsync(id);
                return Ok($"Category {id} was deleated.");
            }
            catch (ShopException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
