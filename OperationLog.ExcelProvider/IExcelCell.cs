using System.Collections.Generic;

namespace OperationLog.ExcelProvider
{
    /// <summary>
    /// Интерфейс контракт провайдера Excel для работы с ячейкой. 
    /// </summary>
    public interface IExcelCell
    {
        /// <summary>
        /// Номер строки.
        /// </summary>
        /// <value>The row.</value>
        int Row { get; }
        /// <summary>
        /// Номер столбца.
        /// </summary>
        /// <value>The column.</value>
        int Column { get; }
        /// <summary>
        /// Значение.
        /// </summary>
        /// <value>The value.</value>
        object Value { get; }
        /// <summary>
        /// Установить значения из таблицы, начиная с этой ячейки.
        /// </summary>
        /// <param name="values">Коллекция коллекций значений (таблица).</param>
        void SetFromTable(IEnumerable<IEnumerable<object>> values);
    }
}