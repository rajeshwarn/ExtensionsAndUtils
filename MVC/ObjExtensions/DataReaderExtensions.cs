using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Shared.Extensions
{
    public static class DataReaderExtensions
    {
        #region Private

        private static readonly Hashtable ClassesDefinition = new Hashtable();

        private static T GetValueOrDefault<T>(this IDataRecord row, int ordinal)
        {
            Type t = typeof(T);
            object value = row.GetValue(ordinal);

            t = Nullable.GetUnderlyingType(t) ?? t;
            return (T)(row.IsDBNull(ordinal) ? default(T) : Convert.ChangeType(value, t));
        }

        private static object InitFromDataReader(this PropertyInfo propertyInfo, IDataReader dr)
        {
            Type businessEntityType = propertyInfo.GetType();
            Hashtable hashtable = new Hashtable();
            PropertyInfo[] properties = businessEntityType.GetProperties();
            foreach (PropertyInfo info in properties)
            {
                hashtable[info.Name.ToUpper()] = info;
            }
            object newObject = businessEntityType.TypeInitializer.Invoke(null);
            for (int index = 0; index < dr.FieldCount; index++)
            {
                PropertyInfo info = (PropertyInfo)
                    hashtable[dr.GetName(index).ToUpper()];
                if ((info != null) && info.CanWrite)
                {
                    info.SetValue(newObject, dr.GetValue(index), null);
                }
            }
            return newObject;
        }

        #endregion

        public static T GetValueOrDefault<T>(this IDataRecord row, string fieldName)
        {
            T val = default(T);

            try
            {
                int ordinal = row.GetOrdinal(fieldName);
                val = row.GetValueOrDefault<T>(ordinal);
            }
            catch (Exception)
            {
            }

            return val;
        }

        public static ICollection<T> MapDataToBusinessEntityCollection<T>(this IDataReader dr) where T : new()
        {
            Type businessEntityType = typeof(T);
            List<T> entitys = new List<T>();
            Hashtable hashtable = new Hashtable();
            PropertyInfo[] properties = businessEntityType.GetProperties();
            foreach (PropertyInfo info in properties)
            {
                hashtable[info.Name.ToUpper()] = info;
            }
            while (dr.Read())
            {
                T newObject = new T();
                for (int index = 0; index < dr.FieldCount; index++)
                {
                    PropertyInfo info = (PropertyInfo) hashtable[dr.GetName(index).ToUpper()];
                    if ((info != null) && info.CanWrite)
                    {
                        if (info.PropertyType.FullName.Contains("PNV.Model"))
                        {
                            info.SetValue(newObject, info.InitFromDataReader(dr), null);
                        }
                        else
                        {
                            var value = dr.GetValue(index);
                            if (!(value is DBNull))
                            {
                                if (info.PropertyType.BaseType == typeof(Enum))
                                {
                                    info.SetValue(newObject, Enum.Parse(info.PropertyType, value.ToString(), true), null);
                                }
                                else if (info.PropertyType == typeof (Boolean))
                                {
                                    info.SetValue(newObject, value.Equals("1"), null);
                                }
                                else
                                {
                                    Type t = info.PropertyType;
                                    t = Nullable.GetUnderlyingType(t) ?? t;

                                    info.SetValue(newObject, Convert.ChangeType(value, t), null);
                                }
                            }
                        }
                    }
                }
                entitys.Add(newObject);
            }
            dr.Close();
            return entitys;
        }

        public static T MapDataToBusinessEntity<T>(this IDataReader dr) where T : new()
        {
            Type businessEntityType = typeof(T);
            PropertyInfo[] properties = businessEntityType.GetProperties();
            Hashtable hashtable = (Hashtable)ClassesDefinition[businessEntityType.FullName];
            if (hashtable == null)
            {
                hashtable = new Hashtable();
                foreach (PropertyInfo info in properties)
                {
                    hashtable[info.Name.ToUpper()] = info;
                }
                ClassesDefinition[businessEntityType.FullName] = hashtable;
            }
            T newObject = new T();
            while (dr.Read())
            {
                for (int index = 0; index < dr.FieldCount; index++)
                {
                    PropertyInfo info = (PropertyInfo)hashtable[dr.GetName(index).ToUpper()];
                    if ((info != null) && info.CanWrite)
                    {
                        var value = dr.GetValue(index);
                        if (!(value is DBNull))
                        {
                            if (info.PropertyType.BaseType == typeof(Enum))
                            {
                                info.SetValue(newObject, Enum.Parse(info.PropertyType, value.ToString(), true), null);
                            }
                            else if (info.PropertyType == typeof(Boolean))
                            {
                                info.SetValue(newObject, value.Equals("1"), null);
                            }
                            else
                            {
                                Type t = info.PropertyType;
                                t = Nullable.GetUnderlyingType(t) ?? t;

                                info.SetValue(newObject, Convert.ChangeType(value, t), null);
                            }
                        }
                    }
                }
            }
            return newObject;
        }
    }
}