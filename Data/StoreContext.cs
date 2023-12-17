using LabBackend.Data.Entities;
using Microsoft.EntityFrameworkCore;
namespace LabBackend.Data
{
    public class StoreContext:DbContext
    {
        public StoreContext() { }
        public StoreContext(DbContextOptions<StoreContext> options):base(options)
        {
        }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Record> Record { get; set; }
    }
}
