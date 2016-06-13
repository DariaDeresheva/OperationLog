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

        private static IKernel CreateApplicationKernel()
        {
            var modules = new INinjectModule[]
            {
                new DatabaseInjection("OperationsDatabase"),
                new ServiceInjection(),
                new TextSearchRuleInjection()
            };
            return new StandardKernel(modules);
        }
    }
}
