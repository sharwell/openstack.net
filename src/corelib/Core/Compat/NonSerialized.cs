#if PORTABLE

namespace System
{
    [AttributeUsageAttribute(AttributeTargets.Field, Inherited = false)]
    internal sealed class NonSerialized : Attribute
    {
    }
}

#endif
