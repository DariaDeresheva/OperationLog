﻿using Ninject.Modules;
using OperationLog.Presentation.Desktop.Model;

namespace OperationLog.Presentation.Desktop.Infrastructure
{
    public class TextSearchRuleInjection : NinjectModule
    {
        public override void Load() => Bind<ITextSearchRule>().To<TextSearchStartsWith>();
    }
}