namespace OperationLog.ExcelProvider.ExcelProvider
{
    public interface IExcelWorksheet
    {
        IExcelCell GetCell(int row, int column);
        IExcelRange GetRange(int rowFrom, int columnFrom, int rowTo, int columnTo);
        void AddPieChart(IExcelCell position, string name, int size, IExcelRange valuesRange, IExcelRange axesRange);
    }
}