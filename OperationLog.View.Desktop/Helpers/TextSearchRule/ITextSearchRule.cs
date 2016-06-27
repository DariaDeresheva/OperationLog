namespace OperationLog.Presentation.Desktop.Helpers.TextSearchRule
{
    /// <summary>
    /// Интерфейс представляет контракт для правил текстового поиска.
    /// </summary>
    public interface ITextSearchRule
    {
        /// <summary>
        /// Удовлетворяет ли значение поисковому запросу.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="searchQuery">Поисковый запрос.</param>
        /// <returns><c>true</c> если удовлетворяет, <c>false</c> иначе.</returns>
        bool SearchSuccesful(string value, string searchQuery);
    }
}
