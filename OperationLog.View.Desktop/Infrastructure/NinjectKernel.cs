using Ninject;
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
            return new StandardKernel(new DatabaseInjection("OperationsDatabase"), new ServiceInjection());
        }
    }
}
