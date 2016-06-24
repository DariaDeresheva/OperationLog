using Ninject.Modules;
using OperationLog.Database.UnitOfWork;

namespace OperationLog.BusinessLogic.Infrastructure.Injections
{
    public class UnitOfWorkInjection : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<OperationDatabaseUnitOfWork>();
        }
    }
}