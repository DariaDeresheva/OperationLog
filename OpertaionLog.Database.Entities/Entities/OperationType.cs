using System.Collections.Generic;

namespace OpertaionLog.Database.Objects.Entities
{
    /// <summary>
    /// Класс является представлением типа операции в базе данных приложения.
    /// </summary>
    public class OperationType
    {
        /// <summary>
        /// Идентификатор типа операции.
        /// </summary>
        public short OperationTypeId { get; set; }
        /// <summary>
        /// Название типа операции.
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// Связанные операции типа операции.
        /// </summary>
        public virtual List<Operation> Operations { get; set; }
    }
}
