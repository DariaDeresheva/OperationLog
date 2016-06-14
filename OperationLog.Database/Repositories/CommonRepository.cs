using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public IEnumerable<T> GetAllWhere(Func<T, bool> predicate) => _database.Set<T>().Where(predicate);

        public T Get(Guid id) => _database.Set<T>().Find(id);

        public void Create(T item) => _database.Set<T>().Add(item);

        public void Update(T item) => _database.Entry(item).State = EntityState.Modified;

        public void Delete(T item) => _database.Set<T>().Remove(item);

        public IEnumerable<T> GetAll() => _database.Set<T>();
    }
}