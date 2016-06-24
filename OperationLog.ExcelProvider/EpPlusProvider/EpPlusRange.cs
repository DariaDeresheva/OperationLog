using OfficeOpenXml;

namespace OperationLog.ExcelProvider.EpPlusProvider
{
    public class EpPlusRange : IExcelRange
    {
        private readonly ExcelRange _range;

        public EpPlusRange(ExcelRange range)
        {
            _range = range;
        }

        public string GetAddress() => _range.Address;
    }
}
