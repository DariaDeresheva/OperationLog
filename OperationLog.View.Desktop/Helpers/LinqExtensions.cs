using System;
using System.Collections.Generic;
using System.Linq;

namespace OperationLog.Presentation.Desktop.Helpers
{
    /// <summary>
    /// Класс содержит методы расширения для LINQ.
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Удалить повторения по селектору.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="collection">Коллекция.</param>
        /// <param name="keySelector">Селектор.</param>
        /// <returns>IEnumerable&lt;TSource&gt;.</returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> collection,
            Func<TSource, TKey> keySelector) => collection.GroupBy(keySelector).Select(group => group.First());
    }
}
