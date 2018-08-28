using Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace BLL.RssFeeds
{
    public class RssCustomReader : XmlTextReader //http://stackoverflow.com/questions/8891047/exceptions-with-datetime-parsing-in-rss-feed-use-syndicationfeed-in-c-sharp
    {
        private bool readingDate = false;
        const string CustomUtcDateTimeFormat = "ddd MMM dd HH:mm:ss Z yyyy"; // Wed Oct 07 08:00:07 GMT 2009
        const string CustomPtDateTimeFormat = "ddd, dd MMM yyyy HH:mm:ss Z"; //Sex, 30 Dez 2016 17:36:57 +0000
        
        public RssCustomReader(Stream s) : base(s) { }

        public RssCustomReader(string inputUri) : base(inputUri) { }

        public override void ReadStartElement()
        {
            if (string.Equals(base.NamespaceURI, string.Empty, StringComparison.InvariantCultureIgnoreCase) &&
                (string.Equals(base.LocalName, "lastBuildDate", StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(base.LocalName, "pubDate", StringComparison.InvariantCultureIgnoreCase)))
            {
                readingDate = true;
            }
            base.ReadStartElement();
        }

        public override void ReadEndElement()
        {
            if (readingDate)
            {
                readingDate = false;
            }
            base.ReadEndElement();
        }

        public override string ReadString()
        {
            try
            {
                if (readingDate)
                {
                    string dateString = base.ReadString();
                    DateTime dt;

                    try
                    {
                        if (!DateTime.TryParse(dateString, out dt))
                            dt = DateTime.ParseExact(dateString, CustomUtcDateTimeFormat, CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        try
                        {
                            if (!DateTime.TryParse(dateString, out dt))
                                dt = DateTime.ParseExact(dateString, CustomPtDateTimeFormat, CultureInfo.InvariantCulture);
                        }
                        catch
                        {
                            dt = Utils.ConvertStringToDate(dateString);
                        }
                    }

                    return dt.ToUniversalTime().ToString("R", CultureInfo.InvariantCulture);
                }
                else
                {
                    return base.ReadString();
                }
            }
            catch
            {
                return base.ReadString();
            }
        }
    }
}
