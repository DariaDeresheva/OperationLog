using Ninject.Modules;
using OperationLog.Database.UnitOfWork;

namespace OperationLog.BusinessLogic.Infrastructure.Injections
{
    public class DatabaseInjection : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<OperationDatabaseUnitOfWork>();
        }
    }
}