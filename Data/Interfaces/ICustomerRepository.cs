using LabBackend.Data.Entities;

namespace LabBackend.Data.Interfaces
{
    public interface ICustomerRepository:IRepository<Customer>
    {

        Task<Customer> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<Customer>> GetAllWithDetailsAsync();
    }
}
