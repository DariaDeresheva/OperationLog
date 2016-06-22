using System.IO;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace OperationLog.ExcelProvider.ExcelProvider.EpPlusProvider
{
    public class EpPlusBook : IExcelBook
    {
        private readonly ExcelPackage _excel;

        private ExcelWorksheets WorkSheets => _excel.Workbook.Worksheets;

        public string FileName { get; }

        public EpPlusBook(string filename)
        {
            FileName = filename;
            _excel = new ExcelPackage(new FileInfo(filename));
        }

        public IExcelWorksheet CreateWorksheet(string name) => new EpPlusWorksheet(WorkSheets.Add(name));

        public void Save() => _excel.SaveAs(new FileInfo(FileName));

        public void SaveAs(string filename) => _excel.SaveAs(new FileInfo(filename));

        public void Dispose()
        {
            ApplyStyles();
            Save();
            _excel.Dispose();
        }

        private void ApplyStyles() => WorkSheets.ToList().ForEach(ApplyStyle);

        private static void ApplyStyle(ExcelWorksheet worksheet)
        {
            worksheet.View.ShowGridLines = false;
            worksheet.Cells.AutoFitColumns();
            worksheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells.Where(cell => cell.Value != null)
                .ToList()
                .ForEach(cell => cell.Style.Border.BorderAround(ExcelBorderStyle.Medium));
        }
    }
}
