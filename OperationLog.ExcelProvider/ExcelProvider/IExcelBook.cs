using System;

namespace OperationLog.ExcelProvider.ExcelProvider
{
    public interface IExcelBook : IDisposable
    {
        IExcelWorksheet CreateWorksheet(string name);
        void SaveAs(string filename);
    }
}
