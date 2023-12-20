using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabBackend.Data.Entities
{
    public class Customer:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [ForeignKey(nameof(Currency))]
        public int CurrencyId {  get; set; }
        public Currency Currency { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }
}
