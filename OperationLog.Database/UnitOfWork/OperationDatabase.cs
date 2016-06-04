﻿using System;
using OperationLog.Database.DatabaseContext;
using OperationLog.Database.Repositories;

namespace OperationLog.Database.UnitOfWork
{
    public class OperationDatabase : IUnitOfWork
    {
        private readonly OperationDatabaseContext _database;

        private bool _disposed;

        public OperationDatabase(string connectionString)
        {
            _database = new OperationDatabaseContext(connectionString);
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new CommonRepository<T>(_database);
        }

        public void SaveChanges()
        {
            _database.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _database.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}