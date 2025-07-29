using AutoMapper;
using Somadhan.Application.Dtos;
using Somadhan.Domain.Modules.Product;

namespace Somadhan.Application.Mappings;
public class BrandMappingProfile : Profile
{
    public BrandMappingProfile()
    {
        CreateMap<Brand, BrandDto>().ReverseMap();
    }
}
