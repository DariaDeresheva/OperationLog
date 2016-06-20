namespace OperationLog.ExcelProvider.ExcelProvider
{
    public interface IExcelProvider
    {
        IExcelBook CreateBook(string filename);
    }
}