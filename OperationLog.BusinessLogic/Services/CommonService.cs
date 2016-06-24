using System;
using System.Collections.Generic;
using System.Linq;
using OperationLog.Database.UnitOfWork;

namespace OperationLog.BusinessLogic.Services
{
    public class CommonService : IService
    {
        private readonly IUnitOfWork _database;

        public CommonService(IUnitOfWork database)
        {
            _database = database;
        }

        public List<T> GetAll<T>() where T : class => _database.GetRepository<T>().GetAll().ToList();

        public List<T> GetAllWhere<T>(Func<T, bool> predicate) where T : class
            => _database.GetRepository<T>().GetAllWhere(predicate).ToList();

        public void Dispose() => _database.Dispose();
    }
}