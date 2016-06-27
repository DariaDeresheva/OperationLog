using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OperationLog.Database.Repositories
{
    /// <summary>
    /// Класс представляет репозиторий для сущностей типа T базы данных.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Repositories.IRepository{T}" />
    public class CommonRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private readonly DbContext _database;

        /// <summary>
        /// Конструктор <see cref="CommonRepository{T}"/>. Инициализация контекста базы данных.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public CommonRepository(DbContext context)
        {
            _database = context;
        }

        /// <summary>
        /// Получить все сущности.
        /// </summary>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public IEnumerable<T> GetAll() => _database.Set<T>();

        /// <summary>
        /// Получить все сущности, удовлетворяющие предикату.
        /// </summary>
        /// <param name="predicate">Предикат.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public IEnumerable<T> GetAllWhere(Func<T, bool> predicate) => GetAll().Where(predicate);
    }
}