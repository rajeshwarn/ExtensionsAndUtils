using System;
using System.Globalization;
using Shared.Attributes;

namespace Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static object DbValueOrDbNull(this object obj)
        {
            object val = DBNull.Value;

            if (obj != null)
            {
                if (obj.GetType().BaseType == typeof(Enum))
                {
                    var dbValueAttr = ((Enum) obj).GetAttribute<DbValueAttribute>();
                    val = dbValueAttr != null ? (object) dbValueAttr.Value : obj.GetHashCode();
                }
                else if (obj is bool)
                {
                    val = Convert.ToInt32(obj).ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    val = obj;
                }
            }

            return val;
        }

        public static bool IsNumeric(this object obj)
        {
            //return obj != null && Regex.IsMatch(obj.ToString(), @"-?\d+(\.\d+)?");
            double temp;
            return obj != null && Double.TryParse(obj.ToString(), out temp);
        }
    }
}