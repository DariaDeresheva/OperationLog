using System.Collections.Generic;

namespace OpertaionLog.Database.Objects.Entities
{
    /// <summary>
    /// Класс является представлением типа пользователя в базе данных приложения.
    /// </summary>
    public class UserType
    {
        /// <summary>
        /// Идентификатор типа пользователя.
        /// </summary>
        public string UserTypeId { get; set; }
        /// <summary>
        /// Название типа пользователя.
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// Связанные пользователи.
        /// </summary>
        public virtual List<User> Users { get; set; } 
    }
}
