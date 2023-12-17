using LabBackend;
using LabBackend.Bussines.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace LabBackeend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly MemoryCacheService<CustomerModel> _memoryCacheService;
        public CustomerController(ILogger<CustomerController> logger, MemoryCacheService<CustomerModel> memoryCacheService)
        {
            _logger = logger;
            _memoryCacheService = memoryCacheService;
        }
        //user/<user_id>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var customer = _memoryCacheService.GetById(id);
                return Ok(customer);
            }
            catch
            {
                return NotFound();
            }
        }
        //users
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var customers = _memoryCacheService.GetAll();
                return Ok(customers);
            }
            catch
            {
                return NotFound();
            }
        }
        //user?name=vasyl
        [HttpPost]
        public IActionResult Post([FromQuery]string name)
        {
            try
            {
                var customer = new CustomerModel
                {
                    Id = _memoryCacheService.index,
                    Name = name
                };
                _memoryCacheService.Add(customer);
                return Ok(customer);
            }
            catch
            {
                return BadRequest();
            }
        }
        //user/<user_id>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _memoryCacheService.DeleteById(id);
                return Ok($"Customer {id} was deleated.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
