using LabBackend.Data.Entities;
using LabBackend.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace LabBackend.Data.Repositories
{
    public class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(StoreContext context) : base(context)
        {
        }
    }
}
