
using System.Threading.Tasks;
using System.Collections.Generic;
using SharedDTO.ControllerDtos;
using SharedDTO;

namespace ServiceLayer.IServices
{
    public interface IProductService
    {
        Task<ApiResponse<List<ProductDto>>> GetAllProducts();
        Task<ApiResponse<ProductDto>> GetProductById(long id);
        Task<ApiResponse<ProductDto>> CreateProduct(CreateProductDto ProductDt, long UserId);
        Task<ApiResponse<ProductDto>> UpdateProduct(UpdateProductDto ProductDto, long UserId);
        Task<ApiResponse<bool>> DeleteProduct(long id, long UserId);
    }
}