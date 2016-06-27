using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;

namespace OperationLog.ExcelProvider.EpPlusProvider
{
    /// <summary>
    /// Класс провайдер Excel для работы с ячейкой.
    /// </summary>
    /// <seealso cref="IExcelCell" />
    public class EpPlusCell : IExcelCell
    {
        /// <summary>
        /// Представление ячейки EpPlus.
        /// </summary>
        private readonly ExcelRange _range;

        /// <summary>
        /// Конструктор <see cref="EpPlusCell"/>. Инициализация ячейки EpPlus.
        /// </summary>
        /// <param name="range">The range.</param>
        public EpPlusCell(ExcelRange range)
        {
            _range = range;
        }

        /// <summary>
        /// Номер строки.
        /// </summary>
        public int Row => _range.Start.Row;
        /// <summary>
        /// Номер столбца.
        /// </summary>
        public int Column => _range.Start.Column;
        /// <summary>
        /// Значение.
        /// </summary>
        public object Value => _range.Value;

        /// <summary>
        /// Установить значения из таблицы, начиная с этой ячейки.
        /// </summary>
        /// <param name="values">Коллекция коллекций значений (таблица).</param>
        public void SetFromTable(IEnumerable<IEnumerable<object>> values)
        {
            _range.LoadFromArrays(values.Select(value => value.ToArray()));
        }
    }
}
