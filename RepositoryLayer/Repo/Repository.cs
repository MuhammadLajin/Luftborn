using IRepository;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RepositoryLayer.Repo
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region props
        private readonly ApplicationDBContext Context;
        private DbSet<T> Entities;

        #endregion

        public Repository(ApplicationDBContext context)
        {
            Context = context;
            Entities = context.Set<T>();
        }

        
        #region Get
        public async Task<T> GetById<r>(r Id)
        {
            return await Entities.FindAsync(Id);
        }

        public async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> filter = null, string includingProperties = "")
        {
            IQueryable<T> query = Entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includingProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetFirstOrDefautAsync(Expression<Func<T, bool>> filter = null, string includingProperties = "")
        {
            IQueryable<T> query = Entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includingProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            T record = await query.FirstOrDefaultAsync();

            if(record != default)
            {
                Context.Entry(record).State = EntityState.Detached;
            }

            return record;
        }

        #endregion

        #region Insert
        public T Insert(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            return Entities.Add(entity).Entity;
        }

        public async Task<T> InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            return (await Entities.AddAsync(entity)).Entity;
        }

        public void BulkInsert(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            Context.AddRangeAsync(entities);
        }
        #endregion

        #region Update
        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Entities.Update(entity);
        }
        public void BulkUpdate(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            Context.UpdateRange(entities);
        }
        #endregion

        #region Delete
        public void HardDelete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Entities.Remove(entity);
        }
        #endregion



        
    }
}
