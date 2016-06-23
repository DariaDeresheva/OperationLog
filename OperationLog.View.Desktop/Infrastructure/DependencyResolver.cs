using Ninject;
using OperationLog.BusinessLogic.Infrastructure.Injections;
using OperationLog.Presentation.Desktop.Infrastructure.Injections;

namespace OperationLog.Presentation.Desktop.Infrastructure
{
    public class DependencyResolver
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
