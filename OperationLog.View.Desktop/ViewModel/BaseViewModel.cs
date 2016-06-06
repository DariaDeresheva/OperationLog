using System.Collections.Generic;
using Ninject;
using OperationLog.BusinessLogic.Services;
using OperationLog.Presentation.Desktop.Infrastructure;
using OpertaionLog.Database.Objects.Entities;

namespace OperationLog.Presentation.Desktop.ViewModel
{
    public class BaseViewModel : ObservableObject
    {
        public IEnumerable<User> Users
        {
            get
            {
                using (var service = NinjectKernel.Kernel.Get<IService<User>>())
                {
                    return service.GetAll();
                }
            }
        }
    }
}
