using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data.Configurations;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using MongoDB.Driver;

namespace Api.CrossCutting.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntities
    {
        public IMongoCollection<T> _mongo;

        public BaseRepository() { }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _mongo.Find(x => x.Id == id).FirstOrDefaultAsync();
                if (result == null) return false;

                _mongo.DeleteOne(book => book.Id == id);
                return true;
            }
            catch (ArgumentException)
            {
                throw;
            }

        }

        public async Task<bool> ExistAsync(Guid id)
        {
            try
            {
                return await _mongo.Find(x => x.Id == id).AnyAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if (item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                }
                item.CreatedAt = DateTime.UtcNow;

                await _mongo.InsertOneAsync(item);

            }
            catch (ArgumentException)
            {
                throw;
            }
            return item;
        }

        //public async Task<IEnumerable<T>> SelectAllPageAsync(int skip, int take)
        //{
        //    try
        //    {
        //        return (IEnumerable<T>)await _mongo.FindAsync(x => true)
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                return await _mongo.Find(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                var listResult = await _mongo.Find(_ => true).ToListAsync();
                return listResult;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var result = await _mongo.Find(x => x.Id == item.Id).FirstOrDefaultAsync();

                if (result == null) return null;

                item.UpdatedAt = DateTime.UtcNow;
                item.CreatedAt = result.CreatedAt;

                await _mongo.ReplaceOneAsync(x => x.Id == item.Id, item);
            }
            catch (Exception)
            {
                throw;
            }
            return item;
        }
    }


}

