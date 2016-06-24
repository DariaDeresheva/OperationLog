using System.Collections.Generic;

namespace OpertaionLog.Database.Objects.Entities
{
    public class Department
    {
        public short DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public virtual List<Operation> Operations { get; set; }
    }
}
