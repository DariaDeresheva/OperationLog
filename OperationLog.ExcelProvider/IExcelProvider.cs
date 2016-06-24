namespace OperationLog.ExcelProvider
{
    public interface IExcelProvider
    {
        IExcelBook CreateBook(string filename);
    }
}