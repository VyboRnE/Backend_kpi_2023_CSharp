using LabBackend.Data.Entities;
using LabBackend.Data.Interfaces;

namespace LabBackend.Data.Repositories
{
    public class RecordRepository:BaseRepository<Record>, IRecordRepository
    {
        public RecordRepository(StoreContext context) : base(context)
        {
        }
    }
}
