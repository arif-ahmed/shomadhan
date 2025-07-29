using AutoMapper;
using Somadhan.Application.Dtos;
using Somadhan.Domain.Modules.Product;

namespace Somadhan.Application.Mappings;

public class ProductCategoryMappingProfile : Profile
{
    public ProductCategoryMappingProfile()
    {
        CreateMap<ProductCategory, ProductCategoryDto>();
    }
}
