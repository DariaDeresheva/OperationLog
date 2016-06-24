using System.Collections.Generic;

namespace OperationLog.ExcelProvider
{
    public interface IExcelCell
    {
        int Row { get; } 
        int Column { get; }
        object Value { get; }
        void SetFromTable(IEnumerable<IEnumerable<object>> values);
    }
}