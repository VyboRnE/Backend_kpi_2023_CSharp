using LabBackend.Data.Entities;

namespace LabBackend.Data.Interfaces
{
    public interface IRecordRepository:IRepository<Record>
    {
        Task<Record> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<Record>> GetAllWithDetailsAsync();
    }
}
