using LabBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabBackeend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }
        //user/<user_id>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var customer = new CustomerModel { Id = id, Name = $"Customer {id}" };
            return Ok(customer);
        }
        //users IEnumerable<CustomerModel>
        [HttpGet]
        public IActionResult Get()
        {
            var customers = Enumerable.Range(1, 5).Select(index => new CustomerModel
            {
                Id = index,
                Name = $"Customer {index}"
            });
            return Ok(customers);
        }
        //user
        [HttpPost]
        public IActionResult Post()
        {
            return Ok("New customer was added");
        }
        //user/<user_id>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Customer {id} was deleated.");
        }
    }
}
