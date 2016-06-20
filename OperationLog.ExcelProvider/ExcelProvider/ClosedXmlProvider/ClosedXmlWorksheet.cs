using ClosedXML.Excel;

namespace OperationLog.ExcelProvider.ExcelProvider.ClosedXmlProvider
{
    public class ClosedXmlWorksheet : IExcelWorksheet
    {
        private readonly IXLWorksheet _worksheet;

        public ClosedXmlWorksheet(IXLWorksheet worksheet)
        {
            _worksheet = worksheet;
        }

        public IExcelCell GetCell(int row, int column) => new ClosedXmlCell(_worksheet.Cell(row, column));

        public void SetCell(IExcelCell cell, object value) => _worksheet.Cell(cell.Row, cell.Column).Value = value;
    }
}
