using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Store.DAL.Repositories
{
    public interface IRepository<T> : IDisposable
    {

        void Create(T obj);
        T Read(int id);
        IEnumerable<T> Read();
        IEnumerable<T> Read(Expression<Func<T, bool>> predicate);
        T Update(int id, T updatedObj);
        void Save();
    }
}
