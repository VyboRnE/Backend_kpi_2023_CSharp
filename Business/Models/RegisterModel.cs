namespace LabBackend.Business.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int? CurrencyId { get; set; }
    }
}
