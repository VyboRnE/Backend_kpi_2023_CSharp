using LabBackend.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using LabBackend.Data.Interfaces;

namespace LabBackend.Data
{
    public class UnitOfWork
    {
        private StoreContext Context { get; set; }
        private ICustomerRepository _customerRepository;

        private ICategoryRepository _categoryRepository;

        private ICurrencyRepository _currencyRepository;

        private IRecordRepository _recordRepository;
        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                    _customerRepository = new CustomerRepository(Context);
                return _customerRepository;
            }
        }

        public ICategoryRepository PersonRepository
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(Context);
                return _categoryRepository;
            }
        }

        public ICurrencyRepository ProductRepository
        {
            get
            {
                if (_currencyRepository == null)
                    _currencyRepository = new CurrencyRepository(Context);
                return _currencyRepository;
            }
        }

        public IRecordRepository ProductCategoryRepository
        {
            get
            {
                if (_recordRepository == null)
                    _recordRepository = new RecordRepository(Context);
                return _recordRepository;
            }
        }
        public UnitOfWork()
        {
            Context = new(new DbContextOptions<StoreContext>());
        }

        public UnitOfWork(StoreContext context)
        {
            Context = context;
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
