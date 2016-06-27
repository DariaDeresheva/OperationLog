using Ninject.Modules;
using OperationLog.ExcelProvider;
using OperationLog.ExcelProvider.EpPlusProvider;

namespace OperationLog.Presentation.Desktop.Infrastructure.Injections
{
    /// <summary>
    /// Класс представляет собой модуль внедрения зависимости для интерфейса <see cref="IExcelProvider"/>.
    /// </summary>
    /// <seealso cref="NinjectModule" />
    public class ExcelProviderInjection : NinjectModule
    {
        /// <summary>
        /// Внедрить зависимость при загрузке модуля.
        /// </summary>
        public override void Load() => Bind<IExcelProvider>().To<EpPlusProvider>();
    }
}
