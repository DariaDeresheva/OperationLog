using System;
using System.Collections.Generic;

namespace OperationLog.BusinessLogic.Services
{
    public interface IService : IDisposable
    {
        List<T> GetAll<T>() where T : class;
        List<T> GetAllWhere<T>(Func<T, bool> predicate) where T : class;
    }
}