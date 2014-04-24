namespace OpenStack.Compat
{
    using System;
    using System.Collections.Generic;

    internal static class UriExtensions
    {
        public static string[] GetSegments(this Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");

            string path = uri.AbsolutePath;
            List<string> segments = new List<string>();
            int index = -1;
            while (true)
            {
                int previous = index;
                index = path.IndexOf('/', index + 1);
                if (index == -1)
                {
                    if (previous < path.Length - 1)
                        segments.Add(path.Substring(previous + 1, path.Length - previous - 1));
                    break;
                }

                segments.Add(path.Substring(previous + 1, index - previous));
            }

            return segments.ToArray();
        }
    }
}
