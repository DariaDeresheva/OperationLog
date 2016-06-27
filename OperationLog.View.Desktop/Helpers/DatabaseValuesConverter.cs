using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace OperationLog.Presentation.Desktop.Helpers
{
    /// <summary>
    /// Класс представляет конвертер значений из базы данных. 
    /// </summary>
    public class DatabaseValuesConverter
    {
        /// <summary>
        /// Получить строковое представление IP-адреса из числового.
        /// </summary>
        /// <param name="ipValue">Числовое представление IP-адреса.</param>
        /// <returns>System.String.</returns>
        public static string GetIpString(int ipValue)
        {
            return new IPAddress(BitConverter.GetBytes(ipValue)).ToString();
        }

        /// <summary>
        /// Получить строковое MAC-адреса из коллекции байт.
        /// </summary>
        /// <param name="macValue">Коллекция байт.</param>
        /// <returns>System.String.</returns>
        public static string GetMacString(IEnumerable<byte> macValue)
        {
            return string.Join("::", macValue.Take(6).Select(value => value.ToString("X2")));
        }
    }
}
