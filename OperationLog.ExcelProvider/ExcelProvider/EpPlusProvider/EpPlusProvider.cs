namespace OperationLog.ExcelProvider.ExcelProvider.EpPlusProvider
{
    public class EpPlusProvider : IExcelProvider
    {
        public IExcelBook CreateBook(string filename) => new EpPlusBook(filename);
    }
}
