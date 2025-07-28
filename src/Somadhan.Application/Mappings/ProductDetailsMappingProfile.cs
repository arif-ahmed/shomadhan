using AutoMapper;
using Somadhan.Application.Dtos;
using Somadhan.Domain.Modules.Product;

namespace Somadhan.Application.Mappings
{
    public class ProductDetailsMappingProfile : Profile
    {
        public ProductDetailsMappingProfile()
        {
            CreateMap<ProductDetails, ProductDetailsDto>().ReverseMap();
        }
    }
}
