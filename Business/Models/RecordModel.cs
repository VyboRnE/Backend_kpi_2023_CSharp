using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabBackend.Business.Models
{
    public class RecordModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CategoryId { get; set; }
        public int? CurrencyId { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal ReceiptSum { get; set; }
    }
}
