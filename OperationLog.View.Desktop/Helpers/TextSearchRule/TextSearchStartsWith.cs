using System;

namespace OperationLog.Presentation.Desktop.Helpers.TextSearchRule
{
    /// <summary>
    /// Класс представляет правило текстового поиска: совпадение в начале строки. 
    /// </summary>
    /// <seealso cref="OperationLog.Presentation.Desktop.Helpers.TextSearchRule.ITextSearchRule" />
    public class TextSearchStartsWith : ITextSearchRule
    {
        /// <summary>
        /// Удовлетворяет ли значение поисковому запросу.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="searchQuery">Поисковый запрос.</param>
        /// <returns><c>true</c> если удовлетворяет, <c>false</c> иначе.</returns>
        public bool SearchSuccesful(string value, string searchQuery)
            => value.StartsWith(searchQuery, StringComparison.InvariantCultureIgnoreCase);
    }
}
