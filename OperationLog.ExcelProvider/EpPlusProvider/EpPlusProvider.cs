namespace OperationLog.ExcelProvider.EpPlusProvider
{
    /// <summary>
    /// Класс является провайдером Excel для работы с книгами. 
    /// </summary>
    /// <seealso cref="IExcelProvider" />
    public class EpPlusProvider : IExcelProvider
    {
        /// <summary>
        /// Создать книгу с названием файла.
        /// </summary>
        /// <param name="filename">Название файла.</param>
        /// <returns>IExcelBook.</returns>
        public IExcelBook CreateBook(string filename) => new EpPlusBook(filename);
    }
}
