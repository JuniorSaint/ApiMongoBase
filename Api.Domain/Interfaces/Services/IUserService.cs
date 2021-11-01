using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Api.Domain.Dtos.Login;
using Api.Domain.Dtos.User;

namespace Api.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserDto> Post(UserCreateDto User);
        Task<UserUpdateResultDto> Put(UserUpdateDto user);
        Task<bool> PatchPassword(UserPasswordUpdateDto user);
        Task<bool> Delete(Guid id);
        Task<UserDto> Get(Guid id);
        Task<IEnumerable<UserDto>> GetAll();
        Task<IEnumerable<UserDto>> GetAllWithPagination(int skip, int take);
        Task<IEnumerable<UserDto>> GetByNamesAsync(string name);
    }
}
