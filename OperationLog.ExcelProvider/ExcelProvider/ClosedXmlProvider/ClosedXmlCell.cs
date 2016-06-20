using ClosedXML.Excel;

namespace OperationLog.ExcelProvider.ExcelProvider.ClosedXmlProvider
{
    public class ClosedXmlCell : IExcelCell
    {
        private readonly IXLCell _cell;

        public ClosedXmlCell(IXLCell cell)
        {
            _cell = cell;
        }

        public int Row => _cell.Address.RowNumber;
        public int Column => _cell.Address.ColumnNumber;
        public object Value => _cell.Value;
    }
}