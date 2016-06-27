using System;

namespace OpertaionLog.Database.Objects.Entities
{
    /// <summary>
    /// Класс является представлением операции в базе данных приложения.
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// Идентификатор операции.
        /// </summary>
        public int OperationId { get; set; }
        /// <summary>
        /// Дата и время операции.
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// MAC-адрес станции.
        /// </summary>
        public string StationAddress { get; set; }
        /// <summary>
        /// IP-адрес станции.
        /// </summary>
        public int StationIpAddress { get; set; }
        /// <summary>
        /// Название таблицы.
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// Пользователь.
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// Тип операции.
        /// </summary>
        public virtual OperationType OperationType { get; set; }
        /// <summary>
        /// Программа.
        /// </summary>
        public virtual Program Program { get; set; }
        /// <summary>
        /// Филиал.
        /// </summary>
        public virtual Department Department { get; set; }
    }
}
