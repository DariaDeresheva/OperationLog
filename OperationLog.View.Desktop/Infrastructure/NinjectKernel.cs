using System.Configuration;
using Ninject;
using Ninject.Modules;
using OperationLog.BusinessLogic.Infrastructure;

namespace OperationLog.Presentation.Desktop.Infrastructure
{
    public class NinjectKernel
    {
        public static IKernel Kernel { get; set; }

        static NinjectKernel()
        {
            Kernel = CreateApplicationKernel();
        }

        private static IKernel CreateApplicationKernel() => new StandardKernel(GetModules());

        private static INinjectModule[] GetModules() => new INinjectModule[]
        {
            new DatabaseInjection(ConfigurationManager.ConnectionStrings["OperationDatabase"].ConnectionString),
            new ServiceInjection(),
            new TextSearchRuleInjection()
        };
    }
}
