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
        public static string GetFilePath(int idProject, string nomeImagem)
        {
            return String.Format(ConfigurationManager.AppSettings["FilesPath"].ToString(), idProject) + nomeImagem;
        }


        public static string GetResource(string resourceKey, string scopeName = null, string resourceDic = null)
        {
            try
            {
                if (string.IsNullOrEmpty(resourceKey))
                    return "";

                if (scopeName == null) scopeName = "";

                scopeName = Utils.RemoveDiacritics(scopeName.ToUpper().Trim().Replace(" ", ""));

                var resource = ProjectResource.ResourceManager.GetString(resourceKey + "_" + scopeName);
                if (String.IsNullOrEmpty(resource)) resource = ProjectResource.ResourceManager.GetString(resourceKey);
                return String.IsNullOrEmpty(resource) ? resourceKey : resource;
            }
            catch {
                try
                {
                    var resource = ProjectResource.ResourceManager.GetString(resourceKey);
                    return String.IsNullOrEmpty(resource) ? resourceKey : resource;
                }
                catch { return resourceKey; }  
            }
        }
    }
}