using System.ComponentModel.DataAnnotations;

namespace LabBackend.Data.Entities
{
    public class Customer:BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
