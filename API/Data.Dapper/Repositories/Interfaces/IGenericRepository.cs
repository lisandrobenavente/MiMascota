﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dapper.Repositories.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<int> InsertAsync(T item);
        Task<int> InsertManyAsync(IEnumerable<T> items);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(string id);
        Task<T> GetFirstOrDefaultByDynamicAsync(dynamic item, string? table = null);
        Task<IEnumerable<T>> GetByDynamicAsync(dynamic item, string? table = null);
        Task UpdateAsync(T item);
        Task SetManyToRemovedAsync();
        Task SetToRemovedByDynamicAsync(dynamic item);
        Task DeleteRowsByDynamicValuesAsync(dynamic item, string? table = null);
        Task<List<TT>> GetFromSql<TT>(string query, object? parameter = null);
    }
}
