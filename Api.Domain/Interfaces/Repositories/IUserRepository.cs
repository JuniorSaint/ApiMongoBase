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
        Task<UserEntity> FindByLoginAsync(LoginDto user); // Seleciona email e pass para poder logar 

        Task<UserEntity> GetByEmailAsync(string email); //seleciona um email específico

        Task<IEnumerable<UserEntity>> SearchByEmailAsync(string email); //seleciona  os emails que tem em trecho da procura

        Task<IEnumerable<UserEntity>> SearchByNameAsync(string name);

        Task<bool> UpdatePasswordAsync(UserPasswordUpdateDto password); // Altera somente a senha
    }
}