using Ninject.Modules;
using OperationLog.Presentation.Desktop.Helpers.TextSearchRule;

namespace OperationLog.Presentation.Desktop.Infrastructure.Injections
{
    /// <summary>
    /// Класс представляет собой модуль внедрения зависимости для интерфейса <see cref="ITextSearchRule"/>.
    /// </summary>
    /// <seealso cref="NinjectModule" />
    public class TextSearchRuleInjection : NinjectModule
    {
        /// <summary>
        /// Внедрить зависимость при загрузке модуля.
        /// </summary>
        public override void Load() => Bind<ITextSearchRule>().To<TextSearchStartsWith>();
    }
}
