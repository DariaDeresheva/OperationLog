﻿using Ninject.Modules;
using OperationLog.ExcelProvider.ExcelProvider;
using OperationLog.ExcelProvider.ExcelProvider.EpPlusProvider;

namespace OperationLog.Presentation.Desktop.Infrastructure.Injections
{
    public class ExcelProviderInjection : NinjectModule
    {
        public override void Load() => Bind<IExcelProvider>().To<EpPlusProvider>();
    }
}