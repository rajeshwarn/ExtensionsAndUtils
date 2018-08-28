using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Framework;
using Framework.Enumerators;
using BLL;

namespace Framework
{
    /// <summary>
    /// Summary description for Thumbnail
    /// </summary>
    public class ResizedThumbnail : IHttpHandler, IRequiresSessionState
    {
        private static List<string> extensionsAllowed = new List<string> { "jpg", "gif", "jpeg", "bmp", "png" };

        public void ProcessRequest(HttpContext context)
        {
            GlobalInfo session = SessionUtils.GetGlobalInfo();
            string fileName = context.Request.QueryString.Get("fileName");
            string originalName = context.Request.QueryString.Get("originalFileName");
            string fullImage = context.Request.QueryString.Get("full");
            string path = Utils.GetFilePathToSave(ConfigurationManager.AppSettings["filePath"].ToString(), session.IdProject, fileName);
            if (File.Exists(path))
            {
                string extension = Path.GetExtension(path).ToLower().Remove(0, 1);
                if (extension.Contains("&"))
                    extension = extension.Substring(0, extension.IndexOf("&"));
				
                context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + originalName);
                context.Response.ContentType = "image/" + Utils.GetImageFormat(extension).ToString();
				
                if (extensionsAllowed.Contains(extension))
                {
                    if (string.IsNullOrEmpty(fullImage))
                    {
                        //context.Response.TransmitFile(path);
                        using (var imgOriginal = Bitmap.FromFile(path))
                        {
                            Image resized = Utils.ResizeImage(imgOriginal, new Size(100, 100), true);
                            resized.Save(context.Response.OutputStream, Utils.GetImageFormat(extension));
                            resized.Dispose();
                        }
                    }
                    else
                        context.Response.TransmitFile(path);
                }
                else
                    context.Response.TransmitFile(path); //context.Response.StatusCode = 404;
            }
            else
            {
                var nomeImagem = "myPicture.jpg";               

                path = Utils.GetFilePathToSave(ConfigurationManager.AppSettings["filePath"].ToString(), session.IdProject, nomeImagem);

                if (File.Exists(path))
                {
                    string extension = Path.GetExtension(path).ToLower().Remove(0, 1);
                    context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + originalName);
                    context.Response.ContentType = "image/" + Utils.GetImageFormat(extension).ToString();
                    if (extensionsAllowed.Contains(extension))
                    {
                        if (string.IsNullOrEmpty(fullImage))
                        {
                            //context.Response.TransmitFile(path);
                            using (var imgOriginal = Bitmap.FromFile(path))
                            {
                                Image resized = Utils.ResizeImage(imgOriginal, new Size(100, 100), true);
                                resized.Save(context.Response.OutputStream, Utils.GetImageFormat(extension));
                                resized.Dispose();
                            }
                        }
                        else
                            context.Response.TransmitFile(path);
                    }
                    else
                        context.Response.TransmitFile(path); //context.Response.StatusCode = 404;
                }
                else
                    context.Response.StatusCode = 404;
            }
                //context.Response.StatusCode = 404;
            //context.Response.WriteFile(path);

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