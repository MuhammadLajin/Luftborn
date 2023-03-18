using DomainLayer.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using SharedDTO.ControllerDtos;
using SharedDTO;
using System.Linq.Expressions;
using System;

namespace IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById<r>(r Id);
        Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> filter = null, string includingProperties = "");
        Task<T> GetFirstOrDefautAsync(Expression<Func<T, bool>> filter = null, string includingProperties = "");
        T Insert(T entity);
        Task<T> InsertAsync(T entity);
        void BulkInsert(List<T> entities);
        void Update(T entity);
        void BulkUpdate(List<T> entities);
        void HardDelete(T entity);
    }
}
