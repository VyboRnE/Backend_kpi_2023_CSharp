using LabBackeend.Controllers;
using LabBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecordController : ControllerBase
    {
        private readonly ILogger<RecordController> _logger;

        public RecordController(ILogger<RecordController> logger)
        {
            _logger = logger;
        }
        //record/<record_id>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var record = new RecordModel
            {
                Id = id,
                CustomerId = id,
                CategoryId = id,
                OrderTime = DateTime.Now,
                ReceiptSum = id + 100
            };
            return Ok(record);
        }
        //record
        [HttpGet]
        public IActionResult Get([FromQuery] int? customerId, int? categoryId)
        {
            if (customerId is not null && categoryId is not null)
            {
                var customers = Enumerable.Range(1, 5).Select(index => new RecordModel
                {
                    Id = index,
                    CustomerId = (int)customerId,
                    CategoryId = (int)categoryId,
                    OrderTime = DateTime.Now,
                    ReceiptSum = index + 100
                });
                return Ok(customers);
            }
            return BadRequest();
        }
        //record
        [HttpPost]
        public IActionResult Post()
        {
            return Ok("New record was added");
        }
        //record/<record_id>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Record {id} was deleated.");
        }
    }
}
