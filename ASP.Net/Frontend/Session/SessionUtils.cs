using Framework;
using Framework.Enumerators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Frontend
{
    public static class SessionUtils
    {
        public static void SetSession(string alias, object obj)
        {
            if (System.Web.HttpContext.Current.Session.GetWithTimeout(alias) == null)
                System.Web.HttpContext.Current.Session.AddWithTimeout(alias, obj, TimeSpan.FromHours(6));
            else
                System.Web.HttpContext.Current.Session[alias] = obj;
        }

        public static object GetSession(string alias)
        {
            var session = System.Web.HttpContext.Current.Session.GetWithTimeout(alias);
            return session != null ? session : null;
        }

        public static void CleanSession(string alias)
        {
            if (System.Web.HttpContext.Current.Session[alias] != null)
                System.Web.HttpContext.Current.Session.Remove(alias);
        }

    }
}