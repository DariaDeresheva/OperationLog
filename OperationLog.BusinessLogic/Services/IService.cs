using System;
using System.Collections.Generic;

namespace OperationLog.BusinessLogic.Services
{
    public interface IService<T> : IDisposable
    {
        List<T> GetAll();
        List<T> Find(Func<T, bool> predicate);
        T Get(Guid id);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}