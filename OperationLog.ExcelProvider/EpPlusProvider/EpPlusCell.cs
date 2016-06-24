using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;

namespace OperationLog.ExcelProvider.EpPlusProvider
{
    public class EpPlusCell : IExcelCell
    {
        private readonly ExcelRange _range;

        public EpPlusCell(ExcelRange range)
        {
            _range = range;
        }

        public int Row => _range.Start.Row;
        public int Column => _range.Start.Column;
        public object Value => _range.Value;

        public void SetFromTable(IEnumerable<IEnumerable<object>> values)
        {
            _range.LoadFromArrays(values.Select(value => value.ToArray()));
        }
    }
}
