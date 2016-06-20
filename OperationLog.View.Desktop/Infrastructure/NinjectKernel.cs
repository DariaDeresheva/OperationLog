using Ninject;
using OperationLog.BusinessLogic.Infrastructure;

namespace OperationLog.Presentation.Desktop.Infrastructure
{
    public class NinjectKernel
    {
        public static IKernel GetAppKernel() => new StandardKernel(
            new DatabaseInjection(),
            new ServiceInjection(),
            new ExcelProviderInjection(),
            new TextSearchRuleInjection());

        public static T Get<T>()
        {
            return GetAppKernel().Get<T>();
        }
    }
}
