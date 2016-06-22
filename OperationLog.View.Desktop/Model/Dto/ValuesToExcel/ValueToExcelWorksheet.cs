using System.Collections.Generic;

namespace OperationLog.Presentation.Desktop.Model.Dto.ValuesToExcel
{
    public class ValueToExcelWorksheet
    {
        public string ProgramName { get; set; }
        public IEnumerable<ValueToExcelUser> Users { get; set; }
    }
}
