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
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> Logger;
        private readonly IUserService UserService;
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            Logger = logger;
            UserService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<UserDto>>>> GetAllUsers()
        {
            try
            {
                Logger.LogInformation("begin GetAllUsers");
                var response = await UserService.GetAllUsers();
                return handleStatus(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "GetAllUsers");
                return BadRequest($"Internal server error with exception{ex.InnerException}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<UserDto>>> GetUserById([Required] long id)
        {
            try
            {
                Logger.LogInformation("begin GetUserById");
                var response = await UserService.GetUserById(id);
                return handleStatus(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "GetUserById");
                return BadRequest($"Internal server error with exception{ex.InnerException}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<UserDto>>> CreateUser(CreateUserDto userDto)
        {
            try
            {
                Logger.LogInformation("begin CreateUser");
                var response = await UserService.CreateUser(userDto);
                return handleStatus(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "CreateUser");
                return BadRequest($"Internal server error with exception{ex.InnerException}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<UserDto>>> UpdateUser(UpdateUserDto userDto)
        {
            try
            {
                Logger.LogInformation("begin UpdateUser");
                var response = await UserService.UpdateUser(userDto);
                return handleStatus(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "UpdateUser");
                return BadRequest($"Internal server error with exception{ex.InnerException}");
            }
        }

        [HttpDelete]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteUser([Required] long id)
        {
            try
            {
                Logger.LogInformation("begin DeleteUser");
                var response = await UserService.DeleteUser(id);
                return handleStatus(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "DeleteUser");
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
