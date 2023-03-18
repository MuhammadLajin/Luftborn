
using System.Threading.Tasks;
using System.Collections.Generic;
using SharedDTO.ControllerDtos;
using SharedDTO;

namespace ServiceLayer.IServices
{
    public interface IUserService
    {
        Task<ApiResponse<List<UserDto>>> GetAllUsers();
        Task<ApiResponse<UserDto>> GetUserById(long id);
        Task<ApiResponse<UserDto>> CreateUser(CreateUserDto userDto);
        Task<ApiResponse<UserDto>> UpdateUser(UpdateUserDto userDto);
        Task<ApiResponse<bool>> DeleteUser(long id);
    
    }
}