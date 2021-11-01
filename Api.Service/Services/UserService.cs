using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.Login;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Repositories;
using Api.Domain.Interfaces.Services;
using Api.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {

        private IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<UserDto> Get(Guid id)
        {
            var entity = await _repository.SelectbyIdtAsync(id);
            return _mapper.Map<UserDto>(entity);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var list = await _repository.SelectAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(list);
        }

        public async Task<IEnumerable<UserDto>> GetAllWithPagination(int skip, int take)
        {
            var list = await _repository.SelectAllWithPaginationAsync(skip, take);
            return _mapper.Map<IEnumerable<UserDto>>(list);
        }

        public async Task<UserDto> Post(UserCreateDto user)
        {
            var passwordHasher = new PasswordHasher<UserCreateDto>();
            user.Password = passwordHasher.HashPassword(user, user.Password);

            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            var result = await _repository.InsertAsync(entity);

            return _mapper.Map<UserDto>(result);
        }

        public async Task<UserUpdateResultDto> Put(UserUpdateDto user)
        {
            var passwordHasher = new PasswordHasher<UserUpdateDto>();
            user.Password = passwordHasher.HashPassword(user, user.Password);

            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);

            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<UserUpdateResultDto>(result);
        }

        public async Task<bool> PatchPassword(UserPasswordUpdateDto user)
        {
            var passwordHasher = new PasswordHasher<UserPasswordUpdateDto>();
            user.Password = passwordHasher.HashPassword(user, user.Password);
            return await _repository.UpdatePasswordAsync(user);
        }

        public async Task<IEnumerable<UserDto>> GetByNamesAsync(string name)
        {
            var list = await _repository.GetByNamesAsync(name);
            return _mapper.Map<IEnumerable<UserDto>>(list);
        }
    }
}

