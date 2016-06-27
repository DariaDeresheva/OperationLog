using System;
using System.Collections.Generic;
using System.Linq;
using OperationLog.Database.UnitOfWork;

namespace OperationLog.BusinessLogic.Services
{
    /// <summary>
    /// Класс является сервисом бизнес-операций.
    /// </summary>
    /// <seealso cref="IService" />
    public class CommonService : IService
    {
        /// <summary>
        /// Единица работы с БД.
        /// </summary>
        private readonly IUnitOfWork _database;

        /// <summary>
        /// Конструктор <see cref="CommonService"/>. Инициализация единицы работы с базой данных.
        /// </summary>
        /// <param name="database">The database.</param>
        public CommonService(IUnitOfWork database)
        {
            _database = database;
        }

        /// <summary>
        /// Получить все сущности типа Т из базы данных.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>List&lt;T&gt;.</returns>
        public List<T> GetAll<T>() where T : class => _database.GetRepository<T>().GetAll().ToList();

        /// <summary>
        /// Получить все сущности типа Т из базы данных, удовлетворяющие предикату.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">Предикат.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public List<T> GetAllWhere<T>(Func<T, bool> predicate) where T : class
            => _database.GetRepository<T>().GetAllWhere(predicate).ToList();

        /// <summary>
        /// Освободить ресурсы контекста базы данных.
        /// </summary>
        public void Dispose() => _database.Dispose();
    }
}