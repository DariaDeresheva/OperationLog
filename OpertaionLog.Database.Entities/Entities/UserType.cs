using System.Collections.Generic;

namespace OpertaionLog.Database.Objects.Entities
{
    public class UserType
    {
        public string UserTypeId { get; set; }
        public string TypeName { get; set; }
        public virtual List<User> Users { get; set; } 
    }
}
