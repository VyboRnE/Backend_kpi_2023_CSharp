using LabBackend.Data.Entities;
using LabBackend.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LabBackend.Data.Repositories
{
    public class CustomerRepository:BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(StoreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Customer>> GetAllWithDetailsAsync()
        {
            return await Table
                .Include(c => c.Currency)
                .ToListAsync();
        }

        public async Task<Customer> GetByIdWithDetailsAsync(int id)
        {
            return await Table
                .Include(c => c.Currency)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
