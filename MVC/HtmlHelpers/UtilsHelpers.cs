using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString ToCSV(this HtmlHelper helper, IEnumerable<string> list)
        {
            return new MvcHtmlString(string.Join(", ", list));
        }
    }
}