namespace OperationLog.ExcelProvider
{
    /// <summary>
    /// Интерфейс является контракт провайдера Excel для работы с книгами.
    /// </summary>
    public interface IExcelProvider
    {
        /// <summary>
        /// Создать книгу с названием файла.
        /// </summary>
        /// <param name="filename">Название файла.</param>
        /// <returns>IExcelBook.</returns>
        IExcelBook CreateBook(string filename);
    }
}