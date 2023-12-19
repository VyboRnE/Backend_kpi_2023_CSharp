using LabBackend.Data.Entities;
using Microsoft.EntityFrameworkCore;
namespace LabBackend.Data
{
    public class StoreContext:DbContext
    {
        public StoreContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=database-backend-labs.ccntnban7dnr.eu-north-1.rds.amazonaws.com,1433;Database=database-backend-labs;Integrated Security=false;User Id=admin;Password=KOIlpI6TGY;TrustServerCertificate=True;");
        }
        public StoreContext(DbContextOptions<StoreContext> options):base(options)
        {
        }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Record> Record { get; set; }
    }
}
