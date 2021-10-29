﻿using Api.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using Api.Domain.Dtos.User;
using Microsoft.AspNetCore.Identity;
using Api.Domain.Dtos.Login;
using Api.CrossCutting.Repository;
using Api.Domain.Interfaces.Repositories;
using MongoDB.Driver;
using Api.Data.Configurations;

namespace Api.Data.Implementations
{
    public class UserImplementation : BaseRepository<UserEntity>, IUserRepository
    {


        public UserImplementation(IDataBaseConfig dataBase)
        {
            var client = new MongoClient(dataBase.ConnectionString);
            var database = client.GetDatabase(dataBase.DataBaseName);
            _mongo = database.GetCollection<UserEntity>("Users");
        }

        public async Task<UserEntity> FindByLoginAsync(LoginDto login)
        {
            try
            {
                var result = await _mongo.FindAsync(u => u.Email.Equals(login.Email) &&
                          u.IsActive.Equals("yes"));

                if (result == null) return null;

                //var validResult = await ValidUpdatePassword(login, result.Password);
                var validResult = true;
                return (UserEntity)(validResult ? result : null);

            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        // to valid the password cripytography
        private async Task<bool> ValidUpdatePassword(LoginDto login, string resultPassword)
        {
            var passwordHasher = new PasswordHasher<LoginDto>();
            var status = passwordHasher.VerifyHashedPassword(login, resultPassword, login.Password);
            switch (status)
            {
                case PasswordVerificationResult.Failed: return false;
                case PasswordVerificationResult.Success: return true;
                case PasswordVerificationResult.SuccessRehashNeeded:
                    await UpdatePasswordAsync(login); return true;
                default: throw new InvalidOperationException();
            }
        }

        public async Task<UserEntity> GetByEmailAsync(string email)
        {
            try
            {
                return await _mongo.Find(u => u.Email.Equals(email)).FirstOrDefaultAsync();
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserEntity>> SearchByEmailAsync(string email)
        {
            try
            {
                var result = await _mongo.Find(x => x.Email.ToLower().Contains(email.ToLower())).ToListAsync();
                return result;

            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserEntity>> SearchByNameAsync(string name)
        {
            try
            {
                var result = await _mongo.Find(x => x.UserName.ToLower().Contains(name.ToLower())).ToListAsync();
                return result;
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public async Task<bool> UpdatePasswordAsync(LoginDto password)
        {
            try
            {
                var result = await _mongo.Find(p => p.Email.Equals(password.Email)).FirstOrDefaultAsync();
                if (result == null) return false;

                password.UpdatedAt = DateTime.UtcNow;
                password.CreatedAt = result.CreatedAt;

                //await _mongo.UpdateOneAsync(result, );
            }

            catch (Exception)
            {
                throw;
            }
            return true;
        }

    }
}