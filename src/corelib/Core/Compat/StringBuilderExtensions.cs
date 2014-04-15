namespace net.openstack.Core.Compat
{
    using System;
    using System.Text;

    /// <summary>
    /// Provides extension methods for the <see cref="StringBuilder"/> class.
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Removes all characters from a <see cref="StringBuilder"/> instance.
        /// </summary>
        /// <remarks>
        /// <see cref="Clear"/> is a convenience method that is equivalent to setting the <see cref="StringBuilder.Length"/>
        /// property to 0 (zero).
        /// <para>
        /// Calling the <see cref="Clear"/> method does not modify the instance's <see cref="StringBuilder.Capacity"/> or <see cref="StringBuilder.MaxCapacity"/> properties.
        /// </para>
        /// </remarks>
        /// <param name="builder"></param>
        /// <returns>An object whose <see cref="StringBuilder.Length"/> is 0 (zero).</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="builder"/> is <see langword="null"/>.</exception>
        public static StringBuilder Clear(this StringBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            builder.Length = 0;
            return builder;
        }
    }
}
