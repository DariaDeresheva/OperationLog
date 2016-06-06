using System;
using System.Collections.Generic;
using System.Linq;
using OperationLog.Database.UnitOfWork;

namespace OperationLog.BusinessLogic.Services
{
    public class CommonService<T> : IService<T> where T : class
    {
        private IUnitOfWork Database { get; }

        public CommonService(IUnitOfWork database)
        {
            Database = database;
        }

        public List<T> GetAll()
        {
            return Database.GetRepository<T>().GetAll().ToList();
        }

        public List<T> Find(Func<T, bool> predicate)
        {
            return Database.GetRepository<T>().Find(predicate).ToList();
        }

        public T Get(Guid id)
        {
            return Database.GetRepository<T>().Get(id);
        }

        public void Create(T item)
        {
            Database.GetRepository<T>().Create(item);
            Database.SaveChanges();
        }

        public void Update(T item)
        {
            Database.GetRepository<T>().Update(item);
            Database.SaveChanges();
        }

        public void Delete(T item)
        {
            Database.GetRepository<T>().Delete(item);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
