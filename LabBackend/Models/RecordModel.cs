using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabBackend.Models
{
    public class RecordModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("CustomerId")]
        public int CustomerId {  get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal ReceiptSum { get; set; }
    }
}
