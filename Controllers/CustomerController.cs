using LabBackend;
using LabBackend.Business.Interfaces;
using LabBackend.Business.Models;
using LabBackend.Business.Validation;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace LabBackeend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        //user/<user_id>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerModel>> GetById(int id)
        {
            try
            {
                var customer = await _customerService.GetByIdAsync(id);
                return Ok(customer);
            }
            catch (ShopException ex)
            {
                return NotFound(ex.Message);
            }
        }
        //users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerModel>>> Get()
        {
            try
            {
                var customers = await _customerService.GetAllAsync();
                return Ok(customers);
            }
            catch (ShopException ex)
            {
                return NotFound(ex.Message);
            }
        }
        //user
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CustomerModel customer)
        {
            try
            {
                await _customerService.AddAsync(customer);
                return Ok(customer);
            }
            catch (ShopException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //user/<user_id>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _customerService.DeleteByIdAsync(id);
                return Ok($"Customer {id} was deleated.");
            }
            catch (ShopException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
