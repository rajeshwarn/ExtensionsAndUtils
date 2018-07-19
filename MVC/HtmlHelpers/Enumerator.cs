using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Shared.HtmlHelper
{
    /// <summary>
    ///     Enumerator custom HTML helper.
    /// </summary>
    public static class Enumerator
    {
        /// <summary>
        /// Returns a text enumeration of the values in a list of text
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="list">An object that contains the text values to enumerate</param>
        public static MvcHtmlString EnumerateText(this System.Web.Mvc.HtmlHelper htmlHelper, IEnumerable<string> list)
        {
            return EnumerateText(htmlHelper, list, null);
        }

        /// <summary>
        /// Returns a text enumeration of the values in a list of text
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="list">An object that contains the text values to enumerate</param>
        /// <param name="separator">Specify the text, character or expression which must separate each text element in the list</param>
        public static MvcHtmlString EnumerateText(this System.Web.Mvc.HtmlHelper htmlHelper, IEnumerable<string> list, string separator)
        {
            //se não for definido nada como separador, é considerada a virgula
            if (String.IsNullOrEmpty(separator) || String.IsNullOrWhiteSpace(separator))
                separator = ", ";
            return new MvcHtmlString(string.Join(separator, list));
        }
    }
}