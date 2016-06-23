using Ninject.Modules;
using OperationLog.Presentation.Desktop.Helpers.TextSearchRule;

namespace OperationLog.Presentation.Desktop.Infrastructure.Injections
{
    public class TextSearchRuleInjection : NinjectModule
    {
        public override void Load() => Bind<ITextSearchRule>().To<TextSearchStartsWith>();
    }
}
