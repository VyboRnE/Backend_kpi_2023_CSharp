using System.ComponentModel.DataAnnotations;

namespace LabBackend.Data.Entities
{
    public class Category:BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
