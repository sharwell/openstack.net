namespace net.openstack.Core.Compat
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class StringEx
    {
        public static bool IsNullOrWhiteSpace(string value)
        {
            return string.IsNullOrEmpty(value) || value.Trim().Length == 0;
        }

        public static string Join(string separator, IEnumerable<string> values)
        {
            if (values == null)
                throw new ArgumentNullException("values");

            StringBuilder builder = new StringBuilder();
            bool first = true;
            foreach (string value in values)
            {
                if (first)
                    first = false;
                else
                    builder.Append(separator);

                builder.Append(value);
            }

            return builder.ToString();
        }

        public static string Join(string separator, params object[] values)
        {
            if (values == null)
                throw new ArgumentNullException("values");

            return Join(separator, values.AsEnumerable());
        }

        public static string Join<T>(string separator, IEnumerable<T> values)
        {
            if (values == null)
                throw new ArgumentNullException("values");

            IEnumerable<string> stringValues = values.Select(i => i != null ? i.ToString() : null);
            return Join(separator, stringValues);
        }
    }
}
