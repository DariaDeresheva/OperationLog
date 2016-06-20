namespace OperationLog.ExcelProvider.ExcelProvider
{
    public interface IExcelWorksheet
    {
        IExcelCell GetCell(int row, int column);
        void SetCell(IExcelCell cell, object value);
    }
}