using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;

namespace OperationLog.ExcelProvider.EpPlusProvider
{
    public class EpPlusWorksheet : IExcelWorksheet
    {
        private readonly ExcelWorksheet _worksheet;

        public EpPlusWorksheet(ExcelWorksheet worksheet)
        {
            _worksheet = worksheet;
        }

        public IExcelCell GetCell(int row, int column) => new EpPlusCell(_worksheet.Cells[row, column]);

        public IExcelRange GetRange(IExcelCell from, IExcelCell to)
            => new EpPlusRange(_worksheet.Cells[from.Row, from.Column, to.Row, to.Column]);

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

        private static void ApplyPieChartStyles(ExcelPieChart chart)
        {
            chart.DataLabel.ShowPercent = true;
            chart.DataLabel.ShowLeaderLines = true;
            chart.DataLabel.ShowCategory = true;
            chart.VaryColors = true;
        }
    }
}
