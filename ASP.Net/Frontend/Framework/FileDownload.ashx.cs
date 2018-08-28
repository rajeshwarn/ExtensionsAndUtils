using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Framework;

namespace Framework
{
    /// <summary>
    /// Summary description for FileDownload
    /// </summary>
    public class FileDownload : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var fileName = context.Request.QueryString.Get("fileName");
                int IdProject = int.Parse(context.Request.QueryString.Get("idProject"));
                var path = Utils.GetFilePathToSave(ConfigurationManager.AppSettings["PathToSave"].ToString(), IdProject, fileName);

                context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                context.Response.ContentType = "application/octet-stream";

                if (File.Exists(path))
                    context.Response.TransmitFile(path);
                else
                    context.Response.StatusCode = 404;

                context.Response.End();
            }
            catch (Exception)
            { throw; }
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