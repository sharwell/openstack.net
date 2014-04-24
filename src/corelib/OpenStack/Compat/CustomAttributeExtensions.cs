#if !NET45PLUS

namespace System.Reflection
{
    using System.Linq;

    /// <summary>
    /// Contains static methods for retrieving custom attributes.
    /// </summary>
    internal static class CustomAttributeExtensions
    {
        /// <summary>
        /// Retrieves a custom attribute of a specified type that is applied to a specified assembly.
        /// </summary>
        /// <typeparam name="T">The type of attribute to search for.</typeparam>
        /// <param name="element">The assembly to inspect.</param>
        /// <returns>A custom attribute that matches <typeparamref name="T"/>, or <see langword="null"/> if no such attribute is found.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="element"/> is <see langword="null"/>.</exception>
        public static T GetCustomAttribute<T>(this Assembly element)
            where T : Attribute
        {
            if (element == null)
                throw new ArgumentNullException("element");

            return element.GetCustomAttributes(typeof(T), false).OfType<T>().FirstOrDefault();
        }
    }
}

#endif
