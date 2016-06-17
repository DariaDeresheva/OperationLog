using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using OperationLog.Database.UnitOfWork;

namespace OperationLog.BusinessLogic.Services
{
    public class CommonService : IService
    {
        private IUnitOfWork Database { get; }

        public CommonService(IUnitOfWork database)
        {
            Database = database;
        }

        public List<T> GetAllWhere<T>(Func<T, bool> predicate) where T : class
            => Database.GetRepository<T>().GetAllWhere(predicate).ToList();

        public T Get<T>(Guid id) where T : class => Database.GetRepository<T>().Get(id);

        public void Create<T>(T item) where T : class
        {
            Database.GetRepository<T>().Create(item);
            Database.SaveChanges();
        }

        public void Update<T>(T item) where T : class
        {
            Database.GetRepository<T>().Update(item);
            Database.SaveChanges();
        }

        public void Delete<T>(T item) where T : class
        {
            Database.GetRepository<T>().Delete(item);
            Database.SaveChanges();
        }

        public List<T> GetAll<T>() where T : class => Database.GetRepository<T>().GetAll().ToList();

        public void Dispose() => Database.Dispose();
    }
}