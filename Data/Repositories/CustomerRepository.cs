using LabBackend.Data.Entities;
using LabBackend.Data.Interfaces;

namespace LabBackend.Data.Repositories
{
    public class CustomerRepository:BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(StoreContext context) : base(context)
        {
        }
    }
}
