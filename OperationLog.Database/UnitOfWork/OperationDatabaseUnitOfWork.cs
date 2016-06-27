using System.Data.Entity;
using OperationLog.Database.DatabaseContext;
using OperationLog.Database.Repositories;

namespace OperationLog.Database.UnitOfWork
{
    /// <summary>
    /// Класс является «оберткой» над контекстом базы данных для обслуживания набора объектов, 
    /// изменяемых в бизнес-транзакции, управления записью изменений и разрешением проблем конкуренции данных. 
    /// </summary>
    /// <seealso cref="IUnitOfWork" />
    public class OperationDatabaseUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Контекст базы данных/
        /// </summary>
        private readonly DbContext _database;

        /// <summary>
        /// Конструктор <see cref="OperationDatabaseUnitOfWork"/>. Инициализация контекста базы данных.
        /// </summary>
        public OperationDatabaseUnitOfWork()
        {
            _database = new OperationDatabaseContext();
        }

        /// <summary>
        /// Получить репозиторий сущностей типа Т.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IRepository&lt;T&gt;.</returns>
        public IRepository<T> GetRepository<T>() where T : class => new CommonRepository<T>(_database);
        /// <summary>
        /// Освободить ресурсы контекста базы данных.
        /// </summary>
        public void Dispose() => _database.Dispose();
    }
}