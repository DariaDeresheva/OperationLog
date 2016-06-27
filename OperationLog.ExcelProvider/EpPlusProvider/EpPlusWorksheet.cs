using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;

namespace OperationLog.ExcelProvider.EpPlusProvider
{
    /// <summary>
    /// Класс является провайдером Excel для работы с листом. 
    /// </summary>
    /// <seealso cref="IExcelWorksheet" />
    public class EpPlusWorksheet : IExcelWorksheet
    {
        /// <summary>
        /// EpPlus лист.
        /// </summary>
        private readonly ExcelWorksheet _worksheet;

        /// <summary>
        /// Конструктор <see cref="EpPlusWorksheet"/>. Инициализация листа EpPlus.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        public EpPlusWorksheet(ExcelWorksheet worksheet)
        {
            _worksheet = worksheet;
        }

        /// <summary>
        /// Вернуть ячейку с номером строки и номером столбца.
        /// </summary>
        /// <param name="row">Номер строки.</param>
        /// <param name="column">Номер столбца.</param>
        /// <returns>IExcelCell.</returns>
        public IExcelCell GetCell(int row, int column) => new EpPlusCell(_worksheet.Cells[row, column]);

        /// <summary>
        /// Вернуть диапазон ячеек.
        /// </summary>
        /// <param name="from">От ячейки.</param>
        /// <param name="to">До ячейки.</param>
        /// <returns>IExcelRange.</returns>
        public IExcelRange GetRange(IExcelCell from, IExcelCell to)
            => new EpPlusRange(_worksheet.Cells[from.Row, from.Column, to.Row, to.Column]);

        /// <summary>
        /// Добавить круговую диаграмму.
        /// </summary>
        /// <param name="position">Положение левого верхнего угла.</param>
        /// <param name="name">Название.</param>
        /// <param name="size">Размер в пикселах.</param>
        /// <param name="valuesRange">Диапазон значений.</param>
        /// <param name="axesRange">Диапазон названий.</param>
        public void AddPieChart(IExcelCell position,
            string name,
            int size,
            IExcelRange valuesRange,
            IExcelRange axesRange)
        {
            var chart = (ExcelPieChart) _worksheet.Drawings.AddChart(name, eChartType.PieExploded3D);
            chart.SetPosition(position.Row, 0, position.Column, 0);
            chart.SetSize(size, size);
            chart.Series.Add(valuesRange.GetAddress(), axesRange.GetAddress());
            chart.Title.Text = name;
            ApplyPieChartStyles(chart);
        }

        /// <summary>
        /// Применить стили к круговой диаграмме EpPlus.
        /// </summary>
        /// <param name="chart">Круговая диаграмма EpPlus.</param>
        private static void ApplyPieChartStyles(ExcelPieChart chart)
        {
            chart.DataLabel.ShowPercent = true;
            chart.DataLabel.ShowLeaderLines = true;
            chart.DataLabel.ShowCategory = true;
            chart.VaryColors = true;
        }
    }
}
