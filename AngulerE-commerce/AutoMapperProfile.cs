using AngulerE_commerce.Models;
using E_commerce.Domain.Admin;
using AutoMapper;

namespace AngulerE_commerce
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<AddProduct, AddProductDto>().ReverseMap();
        }
    }
}
