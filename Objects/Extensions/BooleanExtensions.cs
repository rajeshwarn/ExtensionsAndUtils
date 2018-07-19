using System;
using System.Globalization;

namespace Shared.Extensions
{
    public static class BooleanExtensions
    {
        public static bool GetNullableBoolValue(this bool? value)
        {
            if (value == null)
                return true;
            return bool.Parse(value.ToString());
        }

        public static string ToIntAsString(this bool value)
        {
            return IntAsString(value);
        }

        public static string ToIntAsString(this bool? value)
        {
            return IntAsString(value);
        }

        private static string IntAsString(object value)
        {
            return value != null ? Convert.ToInt32(value).ToString(CultureInfo.InvariantCulture) : null;
        }
    }
}