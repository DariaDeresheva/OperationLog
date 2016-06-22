using System.Collections.Generic;

namespace OperationLog.Presentation.Desktop.Model.Dto.ValuesToExcel
{
    public class ValueToExcelUser
    {
        public string UserName { get; set; }
        public IEnumerable<ValueToExcelOperationType> OperationTypes { get; set; }
    }
}
