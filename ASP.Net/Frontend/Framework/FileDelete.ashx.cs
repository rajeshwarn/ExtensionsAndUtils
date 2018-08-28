using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Framework
{
    /// <summary>
    /// Summary description for FileDelete
    /// </summary>
    public class FileDelete : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string module = context.Request.QueryString.Get("InputName");
                var idProject = int.Parse(context.Request.QueryString.Get("idProject"));

                switch (module)
                {
                    case "myModule":
                        DeleteMyModuleData(context, idProject, module);
                        break;
                        
                    default:
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("No files found");
                        break;
                }
                
            }
            catch (Exception)
            { throw; }
        }

        private void DeleteMyModuleData(HttpContext context, int idProject, string module)
        {
            string oldName = context.Request.QueryString.Get("oldName");

            if (!String.IsNullOrEmpty(oldName))
            {
                string mainPath = WebAppUtils.GetFilesPath(idProject, module);
                string path = mainPath + oldName;

                if (Directory.Exists(mainPath) && File.Exists(path))
                    File.Delete(path);
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}