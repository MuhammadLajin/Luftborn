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
    public class UserService : IUserService
    {
        #region Props
        public IUserRepo UserRepo { get; }
        public IMapper Mapper { get; }
        IUnitOfWork UnitofWork { get; }
        #endregion

        public UserService(IUserRepo userRepo, IMapper mapper, IUnitOfWork unitofWork)
        {
            UserRepo = userRepo;
            Mapper = mapper;
            UnitofWork = unitofWork;
        }

        #region Methods
        
        public async Task<ApiResponse<List<UserDto>>> GetAllUsers()
        {
            ApiResponse<List<UserDto>> response = new ApiResponse<List<UserDto>>();

            try 
            { 
                List<User> userList = await UserRepo.GetWhereAsync(x => x.IsDeleted != true);
                if (userList != default)
                {
                    response.IsValidReponse = true;
                    response.CommandMessage = "return list of all users";
                    List<UserDto> userDtoList = new();
                    foreach (User user in userList)
                    {
                        userDtoList.Add(Mapper.Map<UserDto>(user));
                    }
                    response.Datalist = userDtoList;
                    response.TotalCount = userList.Count;
                    response.Status = (int)SharedEnums.ApiResponseStatus.Success;
                }
            }
            catch (Exception ex)
            {
                HandleExceptionResponse(response, ex.InnerException.ToString());
            }

            return response;
        }

        public async Task<ApiResponse<UserDto>> GetUserById(long id)
        {
            ApiResponse<UserDto> response = new ApiResponse<UserDto>();

            try 
            { 
                User user = await UserRepo.GetById(id);
                if (user != default)
                {
                    response.IsValidReponse = true;
                    response.CommandMessage = "return user";
                    response.Datalist = Mapper.Map<UserDto>(user);
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

        public async Task<ApiResponse<UserDto>> CreateUser(CreateUserDto userDto )
        {            
            var response = IsValidUser(userDto);

            #region Validations
            if (!response.IsValidReponse)
            {
                return response;
            }
            #endregion

            try 
            { 
                User newUser = Mapper.Map<User>(userDto);
                newUser.CreatedAt = DateTime.Now;
                User user = UserRepo.Insert(newUser);
                bool isSuccess  = await UnitofWork.SaveChangesAsync();
                if(isSuccess)
                {
                    response.CommandMessage = "new user has been added";
                    response.Datalist = Mapper.Map<UserDto>(user);
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

        public async Task<ApiResponse<UserDto>> UpdateUser(UpdateUserDto userDto)
        {
            var response = IsValidUser(new CreateUserDto { Name=userDto.Name, deposit = userDto.deposit , RoleId = userDto.RoleId });

            #region Validations
            if (!response.IsValidReponse)
            {
                return response;
            }
            #endregion

            try
            {
                User EditUser = await UserRepo.GetById(userDto.Id);
                if (EditUser != default)
                {
                    EditUser = Mapper.Map<User>(userDto);

                    UserRepo.Update(EditUser);
                    bool isSuccess = await UnitofWork.SaveChangesAsync();
                    if (isSuccess)
                    {
                        response.IsValidReponse = true;
                        response.CommandMessage = "user has been updated";
                        response.Datalist = Mapper.Map<UserDto>(EditUser);
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

        public async Task<ApiResponse<bool>> DeleteUser(long id)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            User DeleteUser = await UserRepo.GetById(id);
            try
            {
                if (DeleteUser != default)
                {
                    UserRepo.HardDelete(DeleteUser);
                    bool isSuccess = await UnitofWork.SaveChangesAsync();
                    if (isSuccess)
                    {
                        response.IsValidReponse = true;
                        response.CommandMessage = "user has been deleted";
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

        #region private Methods
        private ApiResponse<UserDto> IsValidUser(CreateUserDto userDto)
        {
            ApiResponse<UserDto> response = new ApiResponse<UserDto>();
            response.IsValidReponse = true;

            if ((int)SharedEnums.Roles.Seller != userDto.RoleId && (int)SharedEnums.Roles.Buyer != userDto.RoleId)
            {
                response.IsValidReponse = false;
                response.CommandMessage = "Role Id is Wrong";
            }
            else if (userDto.deposit < 0)
            {
                response.IsValidReponse = false;
                response.CommandMessage = "deposit cannot be less than 0";
            }
            return response;
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
