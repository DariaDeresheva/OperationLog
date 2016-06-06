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
        public string TableDescription { get; set; }
        public User User { get; set; }
        public OperationType OperationType { get; set; }
        public Program Program { get; set; }
        public Department Department { get; set; }
    }
}
