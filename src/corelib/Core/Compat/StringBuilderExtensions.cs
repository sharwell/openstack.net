namespace net.openstack.Core.Compat
{
    using System;
    using System.Text;

    public static class StringBuilderExtensions
    {
        public static StringBuilder Clear(this StringBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            builder.Length = 0;
            return builder;
        }
    }
}
