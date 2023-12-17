using LabBackend.Data.Entities;

namespace LabBackend.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task AddAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        void Delete(TEntity entity);
        Task DeleteByIdAsync(int id);
    }
}
