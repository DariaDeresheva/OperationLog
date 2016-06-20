using Ninject.Modules;
using OperationLog.ExcelProvider.ExcelProvider;
using OperationLog.ExcelProvider.ExcelProvider.ClosedXmlProvider;

namespace OperationLog.Presentation.Desktop.Infrastructure
{
    public class ExcelProviderInjection : NinjectModule
    {
        public override void Load() => Bind<IExcelProvider>().To<ClosedXmlProvider>();
    }
}
