using System.ComponentModel.DataAnnotations;

namespace LabBackend.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
