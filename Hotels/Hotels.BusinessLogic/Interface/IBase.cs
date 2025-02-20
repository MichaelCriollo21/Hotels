using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.BusinessLogic.Interface
{
    public interface IBase<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> FindAll(string[] IncludeProperties = null);
        IEnumerable<TEntity> Find(TEntity entity);
        TEntity FindById(int Id);
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
    }
}
