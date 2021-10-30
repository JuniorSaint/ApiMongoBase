using System;
using Api.Domain.Dtos.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            #region User
            CreateMap<UserModel, UserDto>().ReverseMap();
            CreateMap<UserModel, UserCreateDto>().ReverseMap();
            CreateMap<UserModel, UserUpdateDto>().ReverseMap();
            CreateMap<UserModel, UserUpdateResultDto>().ReverseMap();
            CreateMap<UserModel, UserPasswordUpdateDto>().ReverseMap();
            #endregion

        }
    }
}

