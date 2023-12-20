using AutoMapper;
using LabBackend.Business.Interfaces;
using LabBackend.Business.Models;
using LabBackend.Business.Validation;
using LabBackend.Data.Entities;
using LabBackend.Data.Interfaces;

namespace LabBackend.Business.Services
{
    public class RecordService : IRecordService
    {
        private IUnitOfWork UnitOfWork { get; }
        private IMapper Mapper { get; }
        private readonly DateTime _validDate = DateTime.Now.AddYears(-5);
        public RecordService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
        public async Task AddAsync(RecordModel model)
        {
            Validate(model);
            if (model.RCurrencyId is null)
            {
                var cust =await UnitOfWork.CustomerRepository.GetByIdAsync(model.CustomerId);
                model.RCurrencyId = cust.CurrencyId;
            }
            var record = Mapper.Map<Record>(model);
            await UnitOfWork.RecordRepository.AddAsync(record);
            await UnitOfWork.SaveAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await UnitOfWork.RecordRepository.DeleteByIdAsync(id);
            await UnitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<RecordModel>> GetAllAsync()
        {
            var records = await UnitOfWork.RecordRepository.GetAllWithDetailsAsync();
            if (!records.Any())
            {
                throw new ShopException("List is empty.");
            }
            return Mapper.Map<IEnumerable<RecordModel>>(records);
        }

        public async Task<RecordModel> GetByIdAsync(int id)
        {
            var record = await UnitOfWork.RecordRepository.GetByIdWithDetailsAsync(id);
            if (record is null)
            {
                throw new ShopException($"Record with id={id} is not found.");
            }
            return Mapper.Map<RecordModel>(record);
        }
        private void Validate(RecordModel model)
        {
            if (model is null)
            {
                throw new ShopException("Model is null.");
            }
            if (model.OrderTime < _validDate || model.OrderTime > DateTime.Now)
            {
                throw new ShopException($"Not valid order time.");
            }
        }
    }
}
