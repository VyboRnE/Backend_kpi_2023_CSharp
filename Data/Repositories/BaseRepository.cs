using LabBackend.Data.Entities;
using LabBackend.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LabBackend.Data.Repositories
{
    public class BaseRepository<TEntity>: IRepository<TEntity>  where TEntity : BaseEntity
    {
        private readonly StoreContext _storeContext;
        public DbSet<TEntity> Table { get; set; }
        public BaseRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
            Table = _storeContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
        }
        public void Delete(TEntity entity)
        {
            Table.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Table.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await Table.FindAsync(id);
        }


        public async Task DeleteByIdAsync(int id)
        {
            Table.Remove(await  Table.FindAsync(id));
        }
    }
}
