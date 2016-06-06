using System.Collections.Generic;

namespace OpertaionLog.Database.Objects.Entities
{
    public class OperationType
    {
        public short OperationTypeId { get; set; }
        public string TypeName { get; set; }
        public virtual List<Operation> Operations { get; set; }
    }
}
