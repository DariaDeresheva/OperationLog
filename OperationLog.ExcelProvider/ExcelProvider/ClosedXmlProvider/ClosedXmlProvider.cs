namespace OperationLog.ExcelProvider.ExcelProvider.ClosedXmlProvider
{
    public class ClosedXmlProvider : IExcelProvider
    {
        public IExcelBook CreateBook(string filename)
        {
            return new ClosedXmlBook(filename);
        }
    }
}
