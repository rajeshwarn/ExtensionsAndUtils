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
    /// Summary description for FileUpload
    /// </summary>
    public class FileUploadHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Request.Files.Count > 0)
                {
                    string module = context.Request.QueryString.Get("InputName");

                    if (module != null)
                    {
                        HttpPostedFile uploadedfile;

                        var idProject = int.Parse(context.Request.QueryString.Get("idProject"));

                        switch (module)
                        {
                            case "myModule":
                                uploadedfile = UploadMyModuleData(context, idProject, module);
                                break;
                        }
                    }
                }
                else
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("No files found");
                }
            }
            catch
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("No files found");
            }
        }


        private HttpPostedFile UploadMyModuleData(HttpContext context, int idProject, string module)
        {
            HttpPostedFile uploadedfile;
            uploadedfile = context.Request.Files[0];

            if (uploadedfile != null)
            {
                string NewFileName = Guid.NewGuid().ToString() + ".xlsx";
                string OriginalName = uploadedfile.FileName;
                string FileType = uploadedfile.ContentType;
                int FileSize = uploadedfile.ContentLength;

                string mainPath = WebAppUtils.GetFilesPath(idProject, module);
                if (!Directory.Exists(mainPath))
                    Directory.CreateDirectory(mainPath);
                var path = mainPath + NewFileName;


                LogRequest(NewFileName + ", " + FileType + ", " + FileSize);
                uploadedfile.SaveAs(path);


                if (File.Exists(path))
                {
                    context.Response.ContentType = FileType;
                    context.Response.Write("{\"name\":\"" + NewFileName + "\",\"OriginalName\":\"" + OriginalName + "\"}");
                }
                else
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("No files found");
                }
            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("No files found");
            }
            return uploadedfile;
        }


        public bool IsReusable
        {
            get { return false; }
        }

        private void LogRequest(string Log)
        {
            //StreamWriter sw = new StreamWriter("z:\\upload\\" + "\\Log.txt", true);
            //sw.WriteLine(DateTime.Now.ToString() + " - " + Log);
            //sw.Flush();
            //sw.Close();
        }



        private double ConvertBytesToMegabytes(long bytes)
        {
            return Math.Round((bytes / 1024f) / 1024f, 2);
        }

        private double ConvertBytesToKilobytes(long bytes)
        {
            return Math.Round(bytes / 1024f, 2);
        }
    }
}