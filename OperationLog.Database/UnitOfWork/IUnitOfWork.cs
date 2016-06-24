using System;
using OperationLog.Database.Repositories;

namespace OperationLog.Database.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
    }
}
