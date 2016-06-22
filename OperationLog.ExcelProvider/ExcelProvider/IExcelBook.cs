using System;

namespace OperationLog.ExcelProvider.ExcelProvider
{
    public interface IExcelBook : IDisposable
    {
        string FileName { get; }
        IExcelWorksheet CreateWorksheet(string name);
        void Save();
        void SaveAs(string filename);
    }
}
