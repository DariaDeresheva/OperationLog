using System;
using System.Collections.Generic;
using System.Linq;

namespace OperationLog.Presentation.Desktop.Helpers
{
    public static class LinqExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> collection,
            Func<TSource, TKey> keySelector) => collection.GroupBy(keySelector).Select(group => group.First());
    }
}
