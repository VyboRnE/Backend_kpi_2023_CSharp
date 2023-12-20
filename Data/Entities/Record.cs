using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabBackend.Data.Entities
{
    public class Record:BaseEntity
    {
        [ForeignKey(nameof(Category))]
        public int CategoryId {  get; set; }
        public Category Category { get; set; }
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int RCurrencyId { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal ReceiptSum { get; set; }
    }
}
