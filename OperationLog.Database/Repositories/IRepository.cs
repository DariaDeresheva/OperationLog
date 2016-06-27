using System;
using System.Collections.Generic;

namespace OperationLog.Database.Repositories
{
    /// <summary>
    /// Интерфейс является контрактом репозитория для сущностей типа T базы данных.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<out T> where T : class
    {
        /// <summary>
        /// Получить все сущности.
        /// </summary>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// Получить все сущности, удовлетворяющие предикату.
        /// </summary>
        /// <param name="predicate">Предикат.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        IEnumerable<T> GetAllWhere(Func<T, bool> predicate);
    }
}
