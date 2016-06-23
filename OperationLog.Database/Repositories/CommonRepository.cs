using System;
using System.Collections.Generic;
using System.Linq;
using OperationLog.Database.DatabaseContext;

namespace OperationLog.Database.Repositories
{
    public class CommonRepository<T> : IRepository<T> where T : class
    {
        private readonly OperationDatabaseContext _database;

        public CommonRepository(OperationDatabaseContext context)
        {
            _database = context;
        }

        public IEnumerable<T> GetAll() => _database.Set<T>();

        public IEnumerable<T> GetAllWhere(Func<T, bool> predicate) => GetAll().Where(predicate);
    }
}