using Ninject.Modules;
using OperationLog.Database.UnitOfWork;

namespace OperationLog.BusinessLogic.Infrastructure.Injections
{
    /// <summary>
    /// Класс представляет собой модуль внедрения зависимости для интерфейса <see cref="IUnitOfWork"/>.
    /// </summary>
    /// <seealso cref="NinjectModule" />
    public class UnitOfWorkInjection : NinjectModule
    {
        /// <summary>
        /// Внедрить зависимость при загрузке модуля.
        /// </summary>
        public override void Load()
        {
            Bind<IUnitOfWork>().To<OperationDatabaseUnitOfWork>();
        }
    }
}