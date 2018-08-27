using System;

namespace Shared.Extensions
{
    public static class TypeExtensions
    {
        public static Type UnderlyingType(this Type type)
        {
            return Nullable.GetUnderlyingType(type) ?? type;
        }
    }
}