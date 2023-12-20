using LabBackend.Data.Entities;
using LabBackend.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LabBackend.Data.Repositories
{
    public class RecordRepository : BaseRepository<Record>, IRecordRepository
    {
        public RecordRepository(StoreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Record>> GetAllWithDetailsAsync()
        {

            return await Table
                .Include(c => c.Category)
                .Include(p => p.Customer)
                .ToListAsync();
        }

        public async Task<Record> GetByIdWithDetailsAsync(int id)
        {
            return await Table
                .Include(c => c.Category)
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
