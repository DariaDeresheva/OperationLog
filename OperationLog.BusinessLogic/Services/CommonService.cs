using System;
using System.Collections.Generic;
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

        public List<T> GetAll<T>() where T : class => Database.GetRepository<T>().GetAll().ToList();

        public List<T> GetAllWhere<T>(Func<T, bool> predicate) where T : class
            => Database.GetRepository<T>().GetAllWhere(predicate).ToList();

        public void Dispose() => Database.Dispose();
    }
}