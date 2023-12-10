using LabBackeend.Controllers;
using LabBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace LabBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecordController : ControllerBase
    {
        private readonly ILogger<RecordController> _logger;
        private readonly MemoryCacheService<RecordModel> _memoryCacheService;
        public RecordController(ILogger<RecordController> logger, MemoryCacheService<RecordModel> memoryCacheService)
        {
            _logger = logger;
            _memoryCacheService = memoryCacheService;
        }
        //record/<record_id>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var record = _memoryCacheService.GetById(id);
                return Ok(record);
            }
            catch
            {
                return NotFound();
            }
        }
        //record?customerId=1&categoryId=1
        [HttpGet]
        public IActionResult Get([FromQuery] int? customerId, int? categoryId)
        {
            try
            {
                if (customerId == null && categoryId == null) { return BadRequest(); }
                var records = _memoryCacheService.GetAll();
                var filteredRecords = records
                    .Where(x => (x.CustomerId == customerId && x.CategoryId == categoryId) ||
                    (categoryId == null && x.CustomerId == customerId) ||
                    (customerId == null && x.CategoryId == categoryId));
                return Ok(filteredRecords);
            }
            catch
            {
                return NotFound();
            }
        }
        //record?customerId=1&categoryId=1
        [HttpPost]
        public IActionResult Post([FromQuery] int customerId, int categoryId)
        {
            try
            {
                var record = new RecordModel
                {
                    Id = _memoryCacheService.index,
                    CustomerId = customerId,
                    CategoryId = categoryId,
                    OrderTime = DateTime.Now,
                    ReceiptSum = 100 + _memoryCacheService.index
                };
                _memoryCacheService.Add(record);
                return Ok(record);
            }
            catch
            {
                return BadRequest();
            }
        }
        //record/<record_id>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _memoryCacheService.DeleteById(id);
                return Ok($"Record {id} was deleated.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
