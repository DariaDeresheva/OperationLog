using System.IO;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace OperationLog.ExcelProvider.EpPlusProvider
{
    /// <summary>
    /// Класс провайдер Excel для работы с книгой.
    /// </summary>
    /// <seealso cref="IExcelBook" />
    public class EpPlusBook : IExcelBook
    {
        /// <summary>
        /// EpPlus книга.
        /// </summary>
        private readonly ExcelPackage _excel;

        /// <summary>
        /// EpPlus листы.
        /// </summary>
        private ExcelWorksheets WorkSheets => _excel.Workbook.Worksheets;

        /// <summary>
        /// Название книги.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Конструктор <see cref="EpPlusBook"/>. Инициализация книги с названием файла.
        /// </summary>
        /// <param name="filename">Название файла.</param>
        public EpPlusBook(string filename)
        {
            FileName = filename;
            _excel = new ExcelPackage(new FileInfo(filename));
        }

        /// <summary>
        /// Создать лист с названием.
        /// </summary>
        /// <param name="name">Название листа.</param>
        /// <returns>IExcelWorksheet.</returns>
        public IExcelWorksheet CreateWorksheet(string name) => new EpPlusWorksheet(WorkSheets.Add(name));

        /// <summary>
        /// Сохранить книгу.
        /// </summary>
        public void Save() => _excel.SaveAs(new FileInfo(FileName));

        /// <summary>
        /// Сохранить книгу с другим названием файла.
        /// </summary>
        /// <param name="filename">Название файла.</param>
        public void SaveAs(string filename) => _excel.SaveAs(new FileInfo(filename));

        /// <summary>
        /// Освободить ресурсы EpPlus провайдера.
        /// </summary>
        public void Dispose()
        {
            ApplyStyles();
            Save();
            _excel.Dispose();
        }

        /// <summary>
        /// Применить стили к листам книги.
        /// </summary>
        private void ApplyStyles() => WorkSheets.ToList().ForEach(ApplyStyle);

        /// <summary>
        /// Применить стиль к листу книги EpPlus.
        /// </summary>
        /// <param name="worksheet">Лист книги EpPlus.</param>
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
