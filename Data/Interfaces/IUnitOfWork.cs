namespace LabBackend.Data.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }

        ICategoryRepository CategoryRepository { get; }

        ICurrencyRepository CurrencyRepository { get; }

        IRecordRepository RecordRepository { get; }
        Task SaveAsync();
    }
}
