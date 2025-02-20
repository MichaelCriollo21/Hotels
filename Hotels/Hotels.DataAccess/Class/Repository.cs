using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Hotels.DataAccess.Interface;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Hotels.DataAccess.Context;

namespace Hotels.DataAccess.Class
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> dbSet;
        private readonly HotelsContext dbContext;

        public Repository(HotelsContext HotelsContext)
        {
            this.dbContext = HotelsContext;
            this.dbSet = dbContext.Set<TEntity>();
        }
        public virtual void Create(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Create(IEnumerable<TEntity> entity)
        {
            foreach (var item in entity)
            {
                dbSet.Add(item);
            }

        }

        public virtual async Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> predicate, string[] includeProperties = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity?> FindByEntity(Expression<Func<TEntity, bool>> predicate, string[] includeProperties = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public virtual IQueryable<TEntity> FindAll(string[] IncludeProperties = null)
        {
            IQueryable<TEntity> Query = dbSet;
            if (IncludeProperties != null)
            {
                foreach (var inlcudeProperty in IncludeProperties)
                {
                    Query = Query.Include(inlcudeProperty);
                }
            }
            return Query.AsNoTracking();
        }

        public virtual TEntity FindById(int Id)
        {
            return dbSet.Find(Id);
        }

        public virtual void Update(TEntity entity)
        {
            EntityEntry EntityEntry = dbContext.Entry(entity);
            var key = GetPrimaryKey(EntityEntry);
            if (EntityEntry.State == EntityState.Detached)
            {
                TEntity currentEntity = this.FindById(key);
                if (currentEntity != null)
                {
                    EntityEntry AttachedEntry = dbContext.Entry(currentEntity);
                    AttachedEntry.CurrentValues.SetValues(entity);

                }
                else
                {
                    dbSet.Attach(entity);
                    EntityEntry.State = EntityState.Modified;
                }
            }
        }

        private int GetPrimaryKey(EntityEntry entry)
        {
            var myObject = entry.Entity;
            var property =
                myObject.GetType()
                    .GetProperties()
                    .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));
            return (int)property.GetValue(myObject, null);
        }

        public virtual void Delete(TEntity entity)
        {
            EntityEntry dbEntityEntry = dbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                dbSet.Attach(entity);
                dbSet.Remove(entity);
            }
        }

        public virtual void Delete(int Id)
        {
            TEntity entity = FindById(Id);
            if (entity == null) return;
            Delete(entity);
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }

                disposedValue = true;
            }
        }

        void IRepository<TEntity>.Dispose(bool disposing)
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        IQueryable<TEntity> IRepository<TEntity>.Find(Expression<Func<TEntity, bool>> predicate, string[] IncludeProperties)
        {
            throw new NotImplementedException();
        }
    }
}
