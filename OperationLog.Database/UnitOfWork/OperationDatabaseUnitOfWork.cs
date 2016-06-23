using OperationLog.Database.DatabaseContext;
using OperationLog.Database.Repositories;

namespace OperationLog.Database.UnitOfWork
{
    public class OperationDatabaseUnitOfWork : IUnitOfWork
    {
        private readonly OperationDatabaseContext _database;

        public OperationDatabaseUnitOfWork()
        {
            _database = new OperationDatabaseContext();
        }

        public IRepository<T> GetRepository<T>() where T : class => new CommonRepository<T>(_database);

        public void SaveChanges() => _database.SaveChanges();

        public void Dispose() => _database.Dispose();
    }
}