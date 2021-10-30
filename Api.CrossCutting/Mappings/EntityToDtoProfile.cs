using System;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            #region User Dto
            CreateMap<UserDto, UserEntity>().ReverseMap();
            CreateMap<UserCreateDto, UserEntity>().ReverseMap();
            CreateMap<UserUpdateDto, UserEntity>().ReverseMap();
            CreateMap<UserUpdateResultDto, UserEntity>().ReverseMap();
            CreateMap<UserPasswordUpdateDto, UserEntity>().ReverseMap();
            #endregion

        }
    }
}

