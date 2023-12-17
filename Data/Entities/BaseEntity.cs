using System.ComponentModel.DataAnnotations;
namespace LabBackend.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id {  get; set; }
    }
}
