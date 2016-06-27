using Ninject.Modules;
using OperationLog.BusinessLogic.Services;

namespace OperationLog.Presentation.Desktop.Infrastructure.Injections
{
    /// <summary>
    /// Класс представляет собой модуль внедрения зависимости для интерфейса <see cref="IService"/>.
    /// </summary>
    /// <seealso cref="NinjectModule" />
    public class ServiceInjection : NinjectModule
    {
        /// <summary>
        /// Внедрить зависимость при загрузке модуля.
        /// </summary>
        public override void Load() => Bind(typeof(IService)).To(typeof(CommonService));
    }
}
