using AutoMapper;
using LabBackend.Business.Interfaces;
using LabBackend.Business.Validation;
using LabBackend.Business.Models;
using LabBackend.Data.Interfaces;
using LabBackend.Data.Entities;

namespace LabBackend.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork UnitOfWork { get; }
        private IMapper Mapper { get; }
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
        public async Task AddAsync(CategoryModel model)
        {
            Validate(model);
            var category= Mapper.Map<Category>(model);
            await UnitOfWork.CategoryRepository.AddAsync(category);
            await UnitOfWork.SaveAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await UnitOfWork.CategoryRepository.DeleteByIdAsync(id);
            await UnitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
        {
            var categories = await UnitOfWork.CategoryRepository.GetAllAsync();
            if (!categories.Any())
            {
                throw new ShopException("List is empty.");
            }
            return Mapper.Map<IEnumerable<CategoryModel>>(categories);
        }

        public async Task<CategoryModel> GetByIdAsync(int id)
        {
            var category = await UnitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                throw new ShopException($"Category with id={id} is not found.");
            }
            return Mapper.Map<CategoryModel>(category);
        }
        private void Validate(CategoryModel model)
        {
            if (model is null)
            {
                throw new ShopException("Model is null.");
            }
            if (String.IsNullOrEmpty(model.Name))
            {
                throw new ShopException($"Category name cannot be empty.");
            }
        }
    }
}
