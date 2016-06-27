using System;

namespace OperationLog.ExcelProvider
{
    /// <summary>
    /// Интерфейс является контрактом провайдера Excel для работы с книгой.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public interface IExcelBook : IDisposable
    {
        /// <summary>
        /// Название книги.
        /// </summary>
        string FileName { get; }
        /// <summary>
        /// Создать лист с названием.
        /// </summary>
        /// <param name="name">Название листа.</param>
        /// <returns>IExcelWorksheet.</returns>
        IExcelWorksheet CreateWorksheet(string name);
        /// <summary>
        /// Сохранить книгу.
        /// </summary>
        void Save();
        /// <summary>
        /// Сохранить книгу с другим названием файла.
        /// </summary>
        /// <param name="filename">Название файла.</param>
        void SaveAs(string filename);
    }
}
