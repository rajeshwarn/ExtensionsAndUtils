using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Shared.Attributes;

namespace Shared.Extensions
{
    public static class EnumExtensions
    {
        #region Private

        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }

        #endregion

        public static string DisplayName(this Enum value)
        {
            Type enumType = value.GetType();
            var enumValue = Enum.GetName(enumType, value);
            MemberInfo member = enumType.GetMember(enumValue)[0];

            var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            var outString = ((DisplayAttribute)attrs[0]).Name;

            if (((DisplayAttribute)attrs[0]).ResourceType != null)
                outString = ((DisplayAttribute)attrs[0]).GetName();

            return outString;
        }

        public static string GetDescription(this Enum value)
        {
            if (value == null) return "";

            var field = value.GetType().GetField(value.ToString());
            if (field == null) return "";

            try
            {
                var attr = (DisplayAttribute) field.GetCustomAttributes(typeof (DisplayAttribute), false).First();
                return attr.GetName();
            }
            catch (Exception)
            {
                return value.ToString();
            }
        }

        public static string DbValue(this Enum value)
        {
            return value != null ? value.GetAttribute<DbValueAttribute>().Value : null;
        }

        //public static string DbValue(this Gender? value)
        //{
        //    return value != null ? value.GetAttribute<DbValueAttribute>().Value : null;
        //}

        //public static string DbValue(this TimeUnit? value)
        //{
        //    return value != null ? value.GetAttribute<DbValueAttribute>().Value : null;
        //}

        //public static string DbValue(this NoVaccinationMotiveGroup? value)
        //{
        //    return value != null ? value.GetAttribute<DbValueAttribute>().Value : null;
        //}
    }
}