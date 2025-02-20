using System.Linq.Expressions;

namespace Hotels.DataAccess.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, string[] IncludeProperties = null);
        void Dispose(bool disposing);
    }
}
