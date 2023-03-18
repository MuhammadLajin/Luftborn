using AutoMapper;
using DomainLayer.Models;
using SharedDTO.ControllerDtos;

namespace ServiceLayer.Mapping
{
    public class UserMapping : Profile
    {
        /// <summary>
        /// Mapping helper to convert DT Models to DB Models
        /// </summary>
        public UserMapping()
        {
            #region User Mapping
            CreateMap<User,UserDto>()
                .ReverseMap();

            CreateMap<UserDto, User>()
                .ReverseMap();

            CreateMap<CreateUserDto, User>()
                .ReverseMap();

            CreateMap<UpdateUserDto, User>()
                .ReverseMap();
            #endregion
        }
    }
}
