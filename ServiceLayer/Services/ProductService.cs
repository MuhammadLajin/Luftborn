using AutoMapper;
using DomainLayer.Models;
using RepositoryLayer.IRepo;
using RepositoryLayer.UnitOfWork;
using ServiceLayer.IServices;
using SharedDTO;
using SharedDTO.ControllerDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ProductService : IProductService
    {
        #region Props
        IProductRepo ProductRepo { get; }
        IMapper Mapper { get; }
        IUnitOfWork UnitofWork { get; }
        IUserRepo UserRepo { get; }
        #endregion

        public ProductService(IProductRepo productRepo, IUserRepo userRepo, IMapper mapper, IUnitOfWork unitofWork)
        {
            ProductRepo = productRepo;
            Mapper = mapper;
            UnitofWork = unitofWork;
            UserRepo = userRepo;
        }

        #region Methods

        public async Task<ApiResponse<List<ProductDto>>> GetAllProducts()
        {
            ApiResponse<List<ProductDto>> response = new ApiResponse<List<ProductDto>>();

            try
            {
                List<Product> productList = await ProductRepo.GetWhereAsync(x => x.IsDeleted != true);
                if (productList != default)
                {
                    response.IsValidReponse = true;
                    response.CommandMessage = "return list of all products";
                    List<ProductDto> productDtoList = new();
                    foreach (Product product in productList)
                    {
                        productDtoList.Add(Mapper.Map<ProductDto>(product));
                    }
                    response.Datalist = productDtoList;
                    response.TotalCount = productList.Count;
                    response.Status = (int)SharedEnums.ApiResponseStatus.Success;
                }
            }
            catch (Exception ex)
            {
                HandleExceptionResponse(response, ex.InnerException.ToString());
            }
            return response;
        }

        public async Task<ApiResponse<ProductDto>> GetProductById(long id)
        {
            ApiResponse<ProductDto> response = new ApiResponse<ProductDto>();
            try
            {
                Product product = await ProductRepo.GetById(id);
                if (product != default)
                {
                    response.IsValidReponse = true;
                    response.CommandMessage = "return product";
                    response.Datalist = Mapper.Map<ProductDto>(product);
                    response.TotalCount = 1;
                    response.Status = (int)SharedEnums.ApiResponseStatus.Success;
                }
                else
                {
                    response.Status = (int)SharedEnums.ApiResponseStatus.NotFound;
                }
            }
            catch (Exception ex)
            {
                HandleExceptionResponse(response, ex.InnerException.ToString());
            }


            return response;
        }

        public async Task<ApiResponse<ProductDto>> CreateProduct(CreateProductDto productDto, long UserId)
        {
            var response = await IsValidProduct(new ProductDto
            {
                Name = productDto.Name,
                AmountAvailable = productDto.AmountAvailable,
                Cost = productDto.Cost,
                SellerId = productDto.SellerId
            });

            #region Validations
            if (!IsCreatedseller(productDto.SellerId, UserId))
            {
                response.CommandMessage = "User Not allowed";
                return response;
            }
            if (!response.IsValidReponse)
            {
                return response;
            }
            #endregion

            try
            {
                Product newProduct = Mapper.Map<Product>(productDto);
                newProduct.CreatedAt = DateTime.Now;
                Product product = ProductRepo.Insert(newProduct);
                bool isSuccess = await UnitofWork.SaveChangesAsync();
                if (isSuccess)
                {
                    response.CommandMessage = "new product has been added";
                    response.Datalist = Mapper.Map<ProductDto>(product);
                    response.TotalCount = 1;
                    response.Status = (int)SharedEnums.ApiResponseStatus.Created;
                }
            }
            catch (Exception ex)
            {
                HandleExceptionResponse(response, ex.InnerException.ToString());
            }
            return response;
        }

        public async Task<ApiResponse<ProductDto>> UpdateProduct(UpdateProductDto productDto, long UserId)
        {
            var response = await IsValidProduct(new ProductDto
            {
                Name = productDto.Name,
                AmountAvailable = productDto.AmountAvailable,
                Cost = productDto.Cost,
                SellerId = productDto.SellerId
            });

            #region Validations
            if (!IsCreatedseller(productDto.SellerId, UserId))
            {
                response.CommandMessage = "User Not allowed";
                return response;
            }
            if (!response.IsValidReponse)
            {
                return response;
            }
            #endregion

            try
            {
                Product EditProduct = await ProductRepo.GetById(productDto.Id);
                if (EditProduct != default)
                {
                    EditProduct = Mapper.Map<Product>(productDto);

                    ProductRepo.Update(EditProduct);
                    bool isSuccess = await UnitofWork.SaveChangesAsync();
                    if (isSuccess)
                    {
                        response.CommandMessage = "product has been updated";
                        response.Datalist = Mapper.Map<ProductDto>(EditProduct);
                        response.TotalCount = 1;
                        response.Status = (int)SharedEnums.ApiResponseStatus.Success;
                    }
                }
                else
                {
                    response.Status = (int)SharedEnums.ApiResponseStatus.NotFound;
                }
            }
            catch (Exception ex)
            {
                HandleExceptionResponse(response, ex.InnerException.ToString());
            }

            return response;
        }

        public async Task<ApiResponse<bool>> DeleteProduct(long id, long UserId)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                Product DeleteProduct = await ProductRepo.GetById(id);
                if (DeleteProduct != default)
                {
                    #region Validations
                    if (!IsCreatedseller(DeleteProduct.SellerId, UserId))
                    {
                        response.CommandMessage = "User Not allowed";
                        return response;
                    }
                    #endregion

                    ProductRepo.HardDelete(DeleteProduct);
                    bool isSuccess = await UnitofWork.SaveChangesAsync();
                    if (isSuccess)
                    {
                        response.IsValidReponse = true;
                        response.CommandMessage = "product has been deleted";
                        response.Datalist = true;
                        response.TotalCount = 1;
                        response.Status = (int)SharedEnums.ApiResponseStatus.Success;
                    }
                }
                else
                {
                    response.Status = (int)SharedEnums.ApiResponseStatus.NotFound;
                }

            }
            catch (Exception ex)
            {
                HandleExceptionResponse(response, ex.InnerException.ToString());
            }

            return response;
        }

        #endregion

        #region Private Methods
        private async Task<ApiResponse<ProductDto>> IsValidProduct(ProductDto productDto)
        {
            ApiResponse<ProductDto> response = new ApiResponse<ProductDto>();
            response.IsValidReponse = true;

            User Seller = await UserRepo.GetById(productDto.SellerId);
            if (Seller == default || Seller.RoleId != (int)(SharedEnums.Roles.Seller))
            {
                response.IsValidReponse = false;
                response.CommandMessage = "Seller Id Not Exist";
                response.Status = (int)SharedEnums.ApiResponseStatus.NotFound;

            }
            else if (productDto.AmountAvailable < 0 || productDto.Cost < 0)
            {
                response.IsValidReponse = false;
                response.CommandMessage = "Amount or Cost cannot be less than 0";
            }
            return response;
        }

        private bool IsCreatedseller(long createdSeller, long UserId)
        {
            return createdSeller == UserId;
        }
        private void HandleExceptionResponse<T>(ApiResponse<T> response, string message)
        {
            response.IsValidReponse = false;
            response.CommandMessage = $"error raised : {message}";
            response.Datalist = default;
            response.Status = (int)SharedEnums.ApiResponseStatus.BadRequest;
        }


        #endregion
    }
}
