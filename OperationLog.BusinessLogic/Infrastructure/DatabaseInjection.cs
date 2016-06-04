using Ninject.Modules;
using OperationLog.Database.UnitOfWork;

namespace OperationLog.BusinessLogic.Infrastructure
{
    public class DatabaseInjection : NinjectModule
    {
        private readonly string _connectionString;

        public DatabaseInjection(string connection)
        {
            _connectionString = connection;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<OperationDatabase>().WithConstructorArgument(_connectionString);
        }
    }
}
