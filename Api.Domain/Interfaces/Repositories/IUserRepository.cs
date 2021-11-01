using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.Login;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<UserEntity> FindByLoginAsync(LoginDto user); // Select email and password to make login

        Task<UserEntity> GetByEmailAsync(string email); //select one specific email

        Task<IEnumerable<UserEntity>> GetByNamesAsync(string Name); //select a collection of Names that contains the specific string

        Task<bool> UpdatePasswordAsync(UserPasswordUpdateDto password); // Altera somente a senha
    }
}