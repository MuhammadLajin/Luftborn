using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceLayer.IServices;
using SharedDTO;
using SharedDTO.ControllerDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Luftborn.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> Logger;
        private readonly IProductService ProductService;
        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            Logger = logger;
            ProductService = productService;
        }


        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<ProductDto>>>> GetAllProducts()
        {
            try
            {
                Logger.LogInformation("begin GetAllProducts");
                var response = await ProductService.GetAllProducts();
                return handleStatus(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "GetAllProducts");
                return BadRequest($"Internal server error with exception{ex.InnerException}");
            }
        }
        
        [HttpGet]
        public async Task<ActionResult<ApiResponse<ProductDto>>> GetProductById([Required] long id)
        {
            try
            {
                Logger.LogInformation("begin GetProductById");
                var response = await ProductService.GetProductById(id);
                return handleStatus(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "GetProductById");
                return BadRequest($"Internal server error with exception{ex.InnerException}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProductDto>>> CreateProduct(ProductDto productDto, [Required] long UserId)
        {
            try
            {
                Logger.LogInformation("begin CreateProduct");
                var response = await ProductService.CreateProduct(productDto, UserId);
                return handleStatus(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "CreateProduct");
                return BadRequest($"Internal server error with exception{ex.InnerException}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<ProductDto>>> UpdateProduct(UpdateProductDto productDto, [Required] long UserId)
        {
            try
            {
                Logger.LogInformation("begin UpdateProduct");
                var response = await ProductService.UpdateProduct(productDto, UserId);
                return handleStatus(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "UpdateProduct");
                return BadRequest($"Internal server error with exception{ex.InnerException}");
            }
        }

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteProduct([Required] long id, [Required] long UserId)
        {
            try
            {
                Logger.LogInformation("begin DeleteProduct");
                var response = await ProductService.DeleteProduct(id, UserId);
                return handleStatus(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "DeleteProduct");
                return BadRequest($"Internal server error with exception{ex.InnerException}");
            }
        }

        #region Private Methods
        private ActionResult<ApiResponse<T>> handleStatus<T>(ApiResponse<T> response)
        {
            if (response.Status == (int)SharedEnums.ApiResponseStatus.Success)
            {
                return Ok(response);
            }
            else if (response.Status == (int)SharedEnums.ApiResponseStatus.Created)
            {
                return Created("Created", response);
            }
            else if (response.Status == (int)SharedEnums.ApiResponseStatus.NotFound)
            {
                return NotFound();
            }
            else
            {
                return BadRequest(response);
            }
        }

        #endregion
    }
}
