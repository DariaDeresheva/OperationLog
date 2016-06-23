﻿using System;

namespace OperationLog.Presentation.Desktop.Helpers.TextSearchRule
{
    public class TextSearchStartsWith : ITextSearchRule
    {
        public bool SearchSuccesful(string value, string searchQuery)
            => value.StartsWith(searchQuery, StringComparison.InvariantCultureIgnoreCase);
    }
}