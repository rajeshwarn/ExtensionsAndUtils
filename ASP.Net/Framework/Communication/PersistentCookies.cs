using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Framework
{
    public static class PersistentCookies
    {
        // to get persistent cookies for us
        private static extern bool InternetGetCookie(string url, string name, StringBuilder data, ref int dataSize);

        public static string RetrieveIECookiesForUrl(string url)
        {
            StringBuilder cookieHeader = new StringBuilder(new String(' ', 256), 256);
            int datasize = cookieHeader.Length;
            if (!InternetGetCookie(url, null, cookieHeader, ref datasize))
            {
                if (datasize < 0)
                    return String.Empty;
                cookieHeader = new StringBuilder(datasize); // resize with new datasize
                InternetGetCookie(url, null, cookieHeader, ref datasize);
            }
            // result is like this: "KEY=Value; KEY2=what ever"
            return cookieHeader.ToString();
        }

        public static CookieContainer GetCookieContainerForUrl(string url)
        {
            return GetCookieContainerForUrl(new Uri(url));
        }

        public static CookieContainer GetCookieContainerForUrl(Uri url)
        {
            CookieContainer container = new CookieContainer();
            string cookieHeaders = RetrieveIECookiesForUrl(url.AbsoluteUri);
            if (cookieHeaders.Length > 0)
            {
                try
                {
                    container.SetCookies(url, cookieHeaders.Replace(";", ","));
                }
                catch (CookieException) { }
            }
            return container;
        }
    }
}
