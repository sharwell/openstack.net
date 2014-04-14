namespace System.Reflection
{
    using System.Linq;

    internal static class CustomAttributeExtensions
    {
        public static T GetCustomAttribute<T>(this Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            return assembly.GetCustomAttributes(typeof(T), false).OfType<T>().First();
        }
    }
}
