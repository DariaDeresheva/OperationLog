namespace OperationLog.ExcelProvider.ExcelProvider
{
    public interface IExcelWorksheet
    {
        IExcelCell GetCell(int row, int column);
        IExcelRange GetRange(IExcelCell from, IExcelCell to);
        void AddPieChart(IExcelCell position, string name, int size, IExcelRange valuesRange, IExcelRange axesRange);
    }
}