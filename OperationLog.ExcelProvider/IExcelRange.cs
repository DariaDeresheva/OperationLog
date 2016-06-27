namespace OperationLog.ExcelProvider
{
    /// <summary>
    /// Интерфейс контракт провайдера Excel для работы с диапазоном.
    /// </summary>
    public interface IExcelRange
    {
        /// <summary>
        /// Вернуть адрес диапазона в строковом представлении Excel.
        /// </summary>
        /// <returns>System.String.</returns>
        string GetAddress();
    }
}
