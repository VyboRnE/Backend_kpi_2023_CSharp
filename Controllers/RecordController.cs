using LabBackeend.Controllers;
using LabBackend.Business.Interfaces;
using LabBackend.Business.Models;
using LabBackend.Business.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace LabBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RecordController : ControllerBase
    {
        private readonly ILogger<RecordController> _logger;
        private readonly IRecordService _recordService;
        public RecordController(IRecordService recordService)
        {
            _recordService = recordService;
        }
        //record/<record_id>
        [HttpGet("{id}")]
        public async Task<ActionResult<RecordModel>> GetById(int id)
        {
            try
            {
                var record = await _recordService.GetByIdAsync(id);
                return Ok(record);
            }
            catch (ShopException ex)
            {
                return NotFound(ex.Message);
            }
        }
        //record?customerId=1&categoryId=1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordModel>>> Get([FromQuery] int? customerId, int? categoryId)
        {
            try
            {
                if (customerId == null && categoryId == null) { return BadRequest("None of filters was chosen."); }
                var records = await _recordService.GetAllAsync();
                var filteredRecords = records
                    .Where(x => (x.CustomerId == customerId && x.CategoryId == categoryId) ||
                    (categoryId == null && x.CustomerId == customerId) ||
                    (customerId == null && x.CategoryId == categoryId));
                return Ok(filteredRecords);
            }
            catch (ShopException ex)
            {
                return NotFound(ex.Message);
            }
        }
        //record
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RecordModel model)
        {
            try
            {
                await _recordService.AddAsync(model);
                return Ok(model);
            }
            catch (ShopException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //record/<record_id>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _recordService.DeleteByIdAsync(id);
                return Ok($"Record {id} was deleated.");
            }
            catch (ShopException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
