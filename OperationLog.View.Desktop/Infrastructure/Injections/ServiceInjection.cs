using Ninject.Modules;
using OperationLog.BusinessLogic.Services;

namespace OperationLog.Presentation.Desktop.Infrastructure.Injections
{
    public class ServiceInjection : NinjectModule
    {
        public override void Load() => Bind(typeof(IService)).To(typeof(CommonService));
    }
}
