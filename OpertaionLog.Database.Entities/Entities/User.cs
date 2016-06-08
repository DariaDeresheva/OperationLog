using System.Collections.Generic;

namespace OpertaionLog.Database.Objects.Entities
{
    public class User
    {
        public short UserId { get; set; }
        public string UserName { get; set; }
        public virtual UserType UserType { get; set; }
        public List<Operation> Operations { get; set; }
    }
}