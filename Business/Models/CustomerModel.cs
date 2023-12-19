using System.ComponentModel.DataAnnotations;

namespace LabBackend.Business.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CurrencyId { get; set; }
    }
}
