using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotels.DataAccess.Class;
using Hotels.DataAccess.Context;

namespace Hotels.DataAccess.Interface
{
    public interface IUnitOfWork
    {
        void SaveChanges();

        Repository<T> Repository<T>() where T : class;

        HotelsContext GetContext();
    }
}
