using Ninject;
using OperationLog.BusinessLogic.Infrastructure.Injections;
using OperationLog.Presentation.Desktop.Infrastructure.Injections;

namespace OperationLog.Presentation.Desktop.Infrastructure
{
    public class DependencyResolver
    {
        private static IKernel GetDependencyResolverKernel() => new StandardKernel(
            new UnitOfWorkInjection(),
            new ServiceInjection(),
            new ExcelProviderInjection(),
            new TextSearchRuleInjection());

        public static T Get<T>()
        {
            return GetDependencyResolverKernel().Get<T>();
        }
    }
}
