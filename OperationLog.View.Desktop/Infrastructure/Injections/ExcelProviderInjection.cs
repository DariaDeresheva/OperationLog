using Ninject.Modules;
using OperationLog.ExcelProvider;
using OperationLog.ExcelProvider.EpPlusProvider;
using OperationLog.ExcelProvider.ExcelProvider;

namespace OperationLog.Presentation.Desktop.Infrastructure.Injections
{
    public class ExcelProviderInjection : NinjectModule
    {
        public override void Load() => Bind<IExcelProvider>().To<EpPlusProvider>();
    }
}
