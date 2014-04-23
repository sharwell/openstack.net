namespace OpenStack.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public static class ListExtensions
    {
        public static ReadOnlyCollection<T> AsReadOnly<T>(this List<T> list)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            return new ReadOnlyCollection<T>(list);
        }
    }
}
