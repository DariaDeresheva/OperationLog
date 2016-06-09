using System;
using System.Collections.Generic;

namespace OperationLog.Database.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllWhere(Func<T, bool> predicate);
        T Get(Guid id);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}
