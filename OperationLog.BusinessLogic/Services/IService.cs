using System;
using System.Collections.Generic;

namespace OperationLog.BusinessLogic.Services
{
    /// <summary>
    /// Интерфейс является контрактом сервиса бизнес-операций.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public interface IService : IDisposable
    {
        /// <summary>
        /// Получить все сущности типа Т из базы данных.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>List&lt;T&gt;.</returns>
        List<T> GetAll<T>() where T : class;
        /// <summary>
        /// Получить все сущности типа Т из базы данных, удовлетворяющие предикату.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">Предикат.</param>
        /// <returns>List&lt;T&gt;.</returns>
        List<T> GetAllWhere<T>(Func<T, bool> predicate) where T : class;
    }
}