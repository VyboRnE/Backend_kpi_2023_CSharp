using AutoMapper;
using LabBackend.Business.Models;
using LabBackend.Data.Entities;
using System.Linq;
namespace LabBackend.Business
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerModel>()
                .ReverseMap()
                .ForMember(rm => rm.Currency, opt => opt.Ignore());
            CreateMap<Category, CategoryModel>() 
                .ReverseMap();
            CreateMap<Record, RecordModel>()
                .ReverseMap()
                .ForMember(rm => rm.Customer, opt => opt.Ignore())
                .ForMember(rm => rm.Category, opt => opt.Ignore());
        }
    }
}
