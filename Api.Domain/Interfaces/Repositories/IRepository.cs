using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Api.Domain.Entities;
using System.Linq;

namespace Api.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntities
    {
        Task<T> InsertAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<bool> DeleteAsync(Guid id);
        Task<T> SelectbyIdtAsync(Guid id);
        Task<IEnumerable<T>> SelectAllAsync();
        Task<IEnumerable<T>> SelectAllWithPaginationAsync(int skip, int take);
        Task<bool> ExistAsync(Guid id);
    }
}