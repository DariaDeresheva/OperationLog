namespace OperationLog.ExcelProvider.ExcelProvider
{
    public interface IExcelCell
    {
        int Row { get; } 
        int Column { get; }
        object Value { get; }
    }
}