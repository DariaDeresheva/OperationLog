using Ninject;
using OperationLog.BusinessLogic.Infrastructure.Injections;
using OperationLog.Presentation.Desktop.Infrastructure.Injections;

namespace OperationLog.Presentation.Desktop.Infrastructure
{
    /// <summary>
    /// Класс представляет контейнер для разрешения зависимостей.
    /// </summary>
    public class DependencyResolver
    {
        /// <summary>
        /// Получить ядро контейнера.
        /// </summary>
        /// <returns>IKernel.</returns>
        private static IKernel GetDependencyResolverKernel() => new StandardKernel(
            new UnitOfWorkInjection(),
            new ServiceInjection(),
            new ExcelProviderInjection(),
            new TextSearchRuleInjection());

        /// <summary>
        /// Разрешить зависимость типа Т.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>T.</returns>
        public static T Get<T>()
        {
            return GetDependencyResolverKernel().Get<T>();
        }
    }
}
