using System;

namespace OperationLog.Presentation.Desktop.Model
{
    public class TextSearchStartsWith : ITextSearchRule
    {
        public bool SearchSuccesful(string value, string searchQuery)
            => value.StartsWith(searchQuery, StringComparison.InvariantCultureIgnoreCase);
    }
}
