using Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Framework
{
    /// <summary>
    /// Summary description for Thumbnail2MVC
    /// </summary>
    public class FullThumbnail : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string nomeDisco = context.Request.QueryString.Get("fileName");
            int IdProject = int.Parse(context.Request.QueryString.Get("idProject"));
            string path = Utils.GetFilePathToSave(ConfigurationManager.AppSettings["filePath"].ToString(), IdProject, nomeDisco);
            //context.Response.ContentType = Utils.GetMimeType(fileName);
            string extension = Path.GetExtension(path);
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=fileName" + extension);
            context.Response.ContentType = "application/octet-stream";
            if (File.Exists(path))
                context.Response.TransmitFile(path);
            else
                context.Response.StatusCode = 404;
            context.Response.End();
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