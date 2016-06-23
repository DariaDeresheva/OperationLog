using System;
using System.Collections.Generic;

namespace OperationLog.Database.Repositories
{
    public interface IRepository<out T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllWhere(Func<T, bool> predicate);
    }
}
