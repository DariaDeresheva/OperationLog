using System.Collections.Generic;

namespace OpertaionLog.Database.Objects.Entities
{
    /// <summary>
    /// Класс является представлением пользователя в базе данных приложения.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public short UserId { get; set; }
        /// <summary>
        /// ФИО пользователя.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Тип пользователя.
        /// </summary>
        public virtual UserType UserType { get; set; }
        /// <summary>
        /// Связанные операции.
        /// </summary>
        public virtual List<Operation> Operations { get; set; }
    }
}