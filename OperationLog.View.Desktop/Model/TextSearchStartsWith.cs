using System;

namespace OperationLog.Presentation.Desktop.Model
{
    public class TextSearchStartsWith : ITextSearchRule
    {
        public bool SearchSuccesful(string value, string searchQuery)
        {
            return value.StartsWith(searchQuery, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
