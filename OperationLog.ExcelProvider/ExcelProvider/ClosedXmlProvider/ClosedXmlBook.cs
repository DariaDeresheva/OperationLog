using ClosedXML.Excel;

namespace OperationLog.ExcelProvider.ExcelProvider.ClosedXmlProvider
{
    public class ClosedXmlBook : IExcelBook
    {
        private readonly XLWorkbook _workbook = new XLWorkbook();
        private readonly string _filename;

        public ClosedXmlBook(string filename)
        {
            _filename = filename;
        }

        public IExcelWorksheet CreateWorksheet(string name) => new ClosedXmlWorksheet(_workbook.AddWorksheet(name));

        public void SaveAs(string filename) => _workbook.SaveAs(filename);

        public void Dispose()
        {
            _workbook.Worksheets.ForEach(ApplyStyles);
            SaveAs(_filename);
        }

        private static void ApplyStyles(IXLWorksheet worksheet)
        {
            worksheet.Columns().AdjustToContents();
            worksheet.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            worksheet.RangeUsed().Style.Border.InsideBorder = XLBorderStyleValues.Medium;
            worksheet.SetShowGridLines(false);
        }
    }
}
