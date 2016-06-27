using OfficeOpenXml;

namespace OperationLog.ExcelProvider.EpPlusProvider
{
    /// <summary>
    /// Класс провайдер Excel для работы с диапазоном. 
    /// </summary>
    /// <seealso cref="IExcelRange" />
    public class EpPlusRange : IExcelRange
    {
        /// <summary>
        /// Представление диапазона EpPlus.
        /// </summary>
        private readonly ExcelRange _range;

        /// <summary>
        /// Конструктор <see cref="EpPlusRange"/>. Инициализация диапазона EpPlus.
        /// </summary>
        /// <param name="range">Диапазон EpPlus.</param>
        public EpPlusRange(ExcelRange range)
        {
            _range = range;
        }

        /// <summary>
        /// Вернуть адрес диапазона в строковом представлении Excel.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetAddress() => _range.Address;
    }
}
