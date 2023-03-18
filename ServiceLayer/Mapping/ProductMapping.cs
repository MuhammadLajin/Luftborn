using AutoMapper;
using DomainLayer.Models;
using SharedDTO.ControllerDtos;

namespace ServiceLayer.Mapping
{
    public class ProductMapping : Profile
    {
        /// <summary>
        /// Mapping helper to convert DT Models to DB Models
        /// </summary>
        public ProductMapping()
        {
            #region Product Mapping
            CreateMap<Product, ProductDto>()
                .ReverseMap();

            CreateMap<ProductDto, Product>()
                .ReverseMap();

            CreateMap<UpdateProductDto, Product>()
                .ReverseMap();
            #endregion
        }
    }
}
