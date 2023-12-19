using AutoMapper;
using LabBackend.Business.Interfaces;
using LabBackend.Business.Models;
using LabBackend.Business.Validation;
using LabBackend.Data.Entities;
using LabBackend.Data.Interfaces;

namespace LabBackend.Business.Services
{
    public class CustomerService : ICustomerService
    {
        private IUnitOfWork UnitOfWork { get; }
        private IMapper Mapper { get; }
        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
        public async Task AddAsync(CustomerModel model)
        {
            Validate(model);
            if (model.CurrencyId is null)
            {
                model.CurrencyId = 0;
            }
            var customer = Mapper.Map<Customer>(model);
            await UnitOfWork.CustomerRepository.AddAsync(customer);
            await UnitOfWork.SaveAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await UnitOfWork.CustomerRepository.DeleteByIdAsync(id);
            await UnitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<CustomerModel>> GetAllAsync()
        {
            var customers = await UnitOfWork.CustomerRepository.GetAllWithDetailsAsync();
            if (!customers.Any())
            {
                throw new ShopException("List is empty.");
            }
            return Mapper.Map<IEnumerable<CustomerModel>>(customers);
        }

        public async Task<CustomerModel> GetByIdAsync(int id)
        {
            var customer = await UnitOfWork.CustomerRepository.GetByIdWithDetailsAsync(id);
            if (customer is null)
            {
                throw new ShopException($"Customer with id={id} is not found.");
            }
            return Mapper.Map<CustomerModel>(customer);
        }
        private void Validate(CustomerModel model)
        {
            if (model is null)
            {
                throw new ShopException("Model is null.");
            }
            if (String.IsNullOrEmpty(model.Name))
            {
                throw new ShopException($"Customer name cannot be empty.");
            }
        }
    }
}
