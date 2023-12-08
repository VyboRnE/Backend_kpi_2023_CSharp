using System.ComponentModel.DataAnnotations;

namespace LabBackend.Models
{
    public class CustomerModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
