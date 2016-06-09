using System;
using System.Collections.Generic;

namespace OperationLog.BusinessLogic.Services
{
    public interface IService : IDisposable
    {
        List<T> GetAll<T>() where T : class;
        List<T> GetAllWhere<T>(Func<T, bool> predicate) where T : class;
        T Get<T>(Guid id) where T : class;
        void Create<T>(T item) where T : class;
        void Update<T>(T item) where T : class;
        void Delete<T>(T item) where T : class;
    }
}