using LabBackend.Data.Entities;
using LabBackend.Data.Interfaces;

namespace LabBackend.Data.Repositories
{
    public class CategoryRepository:BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(StoreContext context) : base(context)
        {
        }
    }
}
