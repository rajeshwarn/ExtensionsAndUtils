using System;
using System.Linq;
using Shared.Attributes;

namespace Shared.Extensions
{
    public static class StringExtensions
    {
        public static TEnum? NullableEnumFromDbValue<TEnum>(this string value) where TEnum : struct
        {
            TEnum? e = null;

            if (!string.IsNullOrEmpty(value))
            {
                var attr = typeof (TEnum)
                    .GetFields()
                    .FirstOrDefault(f => f.GetCustomAttributes(typeof (DbValueAttribute), false)
                        .Cast<DbValueAttribute>()
                        .Any(a => a.Value.Equals(value, StringComparison.OrdinalIgnoreCase)));

                if (attr != null)
                {
                    e = (TEnum) attr.GetValue(null);
                }
                else
                {
                    
                    e = (TEnum) Enum.Parse(typeof (TEnum), value);
                }
            }

            return e;
        }

        public static bool Contains(this string source, string value, StringComparison comp)
        {
            return source.IndexOf(value, comp) >= 0;
        }

        ///<summary>
        ///Método que verifica se a string é null, se está vazia ou possui apenas um espaço
        ///</summary>
        public static bool IsNullOrAllEmpty(this String source)
        {
            return String.IsNullOrEmpty(source) || String.IsNullOrWhiteSpace(source);
        }

        public static string InitCap(this string s)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
        }

        public static string Rpad(this string s, int num, string sufix)
        {
            for (int i = s.Length; i < num; i++)
                s += sufix;
            return s;
        }

        public static string Lpad(this string s, int num, string prefix)
        {
            string result = "";
            for (int i = s.Length; i < num; i++)
                result += prefix;
            result += s;
            return result;
        }

        public static bool? ToBoolean(this string s)
        {
            string[] t = {"true", "True", "1"};
            string[] f = {"false", "False", "0"};

            if (s == null) return null;
            if (t.Contains(s)) return true;
            if (f.Contains(s)) return false;
            throw new FormatException("The string is not a recognized as a valid boolean value.");
        }
    }
}