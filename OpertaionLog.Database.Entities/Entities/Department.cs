using System.Collections.Generic;

namespace OpertaionLog.Database.Objects.Entities
{
    /// <summary>
    /// Класс является представлением филиала в базе данных приложения.
    /// </summary>
    public class Department
    {
        /// <summary>
        /// Идентификатор филиала.
        /// </summary>
        public short DepartmentId { get; set; }
        /// <summary>
        /// Название филиала.
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Связанные операции.
        /// </summary>
        public virtual List<Operation> Operations { get; set; }
    }
}
