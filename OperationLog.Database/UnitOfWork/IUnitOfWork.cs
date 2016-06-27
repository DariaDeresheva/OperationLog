using System;
using OperationLog.Database.Repositories;

namespace OperationLog.Database.UnitOfWork
{
    /// <summary>
    /// Интерфейс является контрактом для «обертки» над контекстом базы данных для обслуживания набора объектов, 
    /// изменяемых в бизнес-транзакции, управления записью изменений и разрешением проблем конкуренции данных. 
    /// </summary>
    /// <seealso cref="IDisposable" />
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Получить репозиторий сущностей типа Т.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IRepository&lt;T&gt;.</returns>
        IRepository<T> GetRepository<T>() where T : class;
    }
}
