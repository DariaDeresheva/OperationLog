using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OperationLog.Database.Repositories
{
    public class CommonRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _database;

        public CommonRepository(DbContext context)
        {
            _database = context;
        }

        public IEnumerable<T> GetAll() => _database.Set<T>();
        public IEnumerable<T> GetAllWhere(Func<T, bool> predicate) => GetAll().Where(predicate);
    }
}