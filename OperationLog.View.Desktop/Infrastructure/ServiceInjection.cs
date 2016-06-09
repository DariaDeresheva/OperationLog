using Ninject.Modules;
using OperationLog.BusinessLogic.Services;

namespace OperationLog.Presentation.Desktop.Infrastructure
{
    public class ServiceInjection : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IService)).To(typeof(CommonService));
        }
    }
}
