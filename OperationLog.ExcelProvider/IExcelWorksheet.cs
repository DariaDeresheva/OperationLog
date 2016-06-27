namespace OperationLog.ExcelProvider
{
    /// <summary>
    /// Интерфейс является контрактом провайдера Excel для работы с листом.
    /// </summary>
    public interface IExcelWorksheet
    {
        /// <summary>
        /// Вернуть ячейку с номером строки и номером столбца.
        /// </summary>
        /// <param name="row">Номер строки.</param>
        /// <param name="column">Номер столбца.</param>
        /// <returns>IExcelCell.</returns>
        IExcelCell GetCell(int row, int column);
        /// <summary>
        /// Вернуть диапазон ячеек.
        /// </summary>
        /// <param name="from">От ячейки.</param>
        /// <param name="to">До ячейки.</param>
        /// <returns>IExcelRange.</returns>
        IExcelRange GetRange(IExcelCell from, IExcelCell to);
        /// <summary>
        /// Добавить круговую диаграмму.
        /// </summary>
        /// <param name="position">Положение левого верхнего угла.</param>
        /// <param name="name">Название.</param>
        /// <param name="size">Размер в пикселах.</param>
        /// <param name="valuesRange">Диапазон значений.</param>
        /// <param name="axesRange">Диапазон названий.</param>
        void AddPieChart(IExcelCell position, string name, int size, IExcelRange valuesRange, IExcelRange axesRange);
    }
}