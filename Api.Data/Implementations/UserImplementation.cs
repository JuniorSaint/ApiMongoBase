using Api.Domain.Entities;
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
                var result = await _mongo.Find(u => u.Email.Equals(login.Email) &&
                          u.IsActive.Equals("yes")).FirstOrDefaultAsync();

                if (result == null) return null;

                var validResult = ValidUpdatePassword(login, result.Password);
                return (UserEntity)(validResult ? result : null);

            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        // to valid the password cripytography
        private bool ValidUpdatePassword(LoginDto login, string resultPassword)
        {
            var passwordHasher = new PasswordHasher<LoginDto>();
            var status = passwordHasher.VerifyHashedPassword(login, resultPassword, login.Password);
            switch (status)
            {
                case PasswordVerificationResult.Failed: return false;
                case PasswordVerificationResult.Success: return true;
                case PasswordVerificationResult.SuccessRehashNeeded: return true;
                default: throw new InvalidOperationException();
            }
        }

        public async Task<UserEntity> GetByEmailAsync(string email)
        {
            try
            {
                return await _mongo.Find(u => u.Email.ToLower().Equals(email.ToLower())).FirstOrDefaultAsync();
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public async Task<bool> UpdatePasswordAsync(UserPasswordUpdateDto password)
        {
            try
            {
                var result = await _mongo.Find(p => p.Email.Equals(password.Email)).FirstOrDefaultAsync();
                if (result == null) return false;
                var update = Builders<UserEntity>.Update.Set(_ => _.Password, password.Password)
                                                        .Set(_ => _.UpdatedAt, DateTime.UtcNow)
                                                        .Set(_ => _.CreatedAt, result.CreatedAt);

                var finalResult = await _mongo.UpdateOneAsync(_ => _.Email == password.Email, update);
            }

            catch (Exception)
            {
                throw;
            }
            return true;
        }

        public async Task<IEnumerable<UserEntity>> GetByNamesAsync(string Name)
        {
            try
            {
                var result = await _mongo.Find(x => x.UserName.ToLower().Contains(Name.ToLower())).ToListAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }


        }



        //private FilterDefinition<UserEntity> GetFilter(UserDto userFilter)
        //{
        //    FilterDefinition<UserEntity> filter = FilterDefinition<UserEntity>.Empty;

        //    if (!string.IsNullOrEmpty(userFilter.Email))
        //    {
        //        filter = Builders<UserEntity>.Filter.Regex("Email", new MongoDB.Bson.BsonRegularExpression(userFilter.Email));
        //    }

        //    if (!string.IsNullOrEmpty(userFilter.UserName))
        //    {
        //        filter = Builders<UserEntity>.Filter.Regex("UserName", new MongoDB.Bson.BsonRegularExpression(userFilter.UserName));
        //    }

        //    if (!string.IsNullOrEmpty(userFilter.IsActive))
        //    {
        //        filter = Builders<UserEntity>.Filter.Regex("IsActive", new MongoDB.Bson.BsonRegularExpression(userFilter.IsActive));
        //    }

        //    return filter;
        //}
    }
}