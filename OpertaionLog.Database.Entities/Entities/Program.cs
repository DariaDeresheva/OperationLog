using System.Collections.Generic;

namespace OpertaionLog.Database.Objects.Entities
{
    /// <summary>
    /// Класс является представлением программы в базе данных приложения.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Идентификатор программы.
        /// </summary>
        public string ProgramId { get; set; }
        /// <summary>
        /// Название программы.
        /// </summary>
        public string ProgramName { get; set; }
        /// <summary>
        /// Связанные операции.
        /// </summary>
        public virtual List<Operation> Operations { get; set; }
    }
}
