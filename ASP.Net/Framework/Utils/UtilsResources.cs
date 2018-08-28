using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Framework.Resources
{
    public class UtilsResources : System.Web.UI.UserControl
    {
        public string GetGlobalResource(string resourceKey, string resourceDic = null)
        {
            if (string.IsNullOrEmpty(resourceKey))
                return "";
            return (String)GetGlobalResourceObject(String.IsNullOrEmpty(resourceDic) ? "GlobalResource" : resourceDic, resourceKey);
        }
    }
}
