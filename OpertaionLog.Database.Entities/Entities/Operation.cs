using System;

namespace OpertaionLog.Database.Objects.Entities
{
    public class Operation
    {
        public int OperationId { get; set; }
        public DateTime DateTime { get; set; }
        public string StationAddress { get; set; }
        public int StationIpAddress { get; set; }
        public string TableName { get; set; }
        public virtual User User { get; set; }
        public virtual OperationType OperationType { get; set; }
        public virtual Program Program { get; set; }
        public virtual Department Department { get; set; }
    }
}
