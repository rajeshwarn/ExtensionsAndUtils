using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Framework
{
    public class ImageUtilsObj
    {
        public string Base64 { get; set; }

        public string Path { get; set; }

        public Image ImagemDrawing { get; set; }

        public long Length { get; set; }
    }

    public static class ImageUtils
    {
        public static string LoadUserControlToEmail<T>(Page page, T uc) where T : UserControl
        {
            uc.ID = Guid.NewGuid().ToString();
            var form = new HtmlForm();
            form.Controls.Add((T)uc);
            page.Controls.Add(form);
            var sw = new StringWriter();
            HttpContext.Current.Server.Execute(page, sw, false);
            sw.Close();
            var toReturn = CleanHtml(sw.ToString());

            toReturn = toReturn.Replace("\r", String.Empty)
                            .Replace("\n", String.Empty)
                            .Replace("\t", String.Empty)
                            .Replace("\"", "'");

            return toReturn;
        }


        public static string CleanHtml(string html)
        {
            html = html.Replace("theForm", "something");
            html = html.Replace("__doPostBack", "__something");
            html = Regex.Replace(html, "<input[^>]*id=\"(__VIEWSTATE)\"[^>]*>", string.Empty, RegexOptions.IgnoreCase);
            html = Regex.Replace(html, "<input[^>]*id=\"(__EVENTVALIDATION)\"[^>]*>", string.Empty, RegexOptions.IgnoreCase);

            html = Regex.Replace(html, "<input[^>]*id=\"(__EVENTTARGET)\"[^>]*>", string.Empty, RegexOptions.IgnoreCase);
            html = Regex.Replace(html, "<input[^>]*id=\"(__EVENTARGUMENT)\"[^>]*>", string.Empty, RegexOptions.IgnoreCase);


            html = Regex.Replace(html, @"<[/]?(form|[ovwxp]:\w+)[^>]*?>", "", RegexOptions.IgnoreCase);

            html = html.Replace("\r", String.Empty);
            html = html.Replace("\n", String.Empty);
            html = html.Replace("\t", String.Empty);
           

            return html;
        }

        
        private static String HexConverter(Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        private static String RGBConverter(Color c)
        {
            return "RGB(" + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + ")";
        }


        public static byte[] GetImageFromUrl(string url)
        {
            Stream stream = null;
            byte[] buf;

            try
            {
                var myProxy = new WebProxy();
                var req = (HttpWebRequest)WebRequest.Create(url);

                var response = (HttpWebResponse)req.GetResponse();
                stream = response.GetResponseStream();

                using (BinaryReader br = new BinaryReader(stream))
                {
                    int len = (int)(response.ContentLength);
                    buf = br.ReadBytes(len);
                    br.Close();
                }

                response.Close();
                stream.Close();
            }
            catch (Exception)
            {
                buf = null;
            }
            return (buf);
        }

        public static string ToBase64(string imgUrl)
        {
            var _sb = new StringBuilder();
            Byte[] _byte = ImageUtils.GetImageFromUrl(imgUrl);
            if (!_byte.SafeAny())
                return "";
            _sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));
            var img = _sb.ToString();

            if (!img.StartsWith("data:"))
            {
                var extensao = imgUrl.IndexOf(".") >= 0 && imgUrl.Count() > imgUrl.LastIndexOf(".") + 1 ? imgUrl.Substring(imgUrl.LastIndexOf(".") + 1) : "";
                img = String.Format("data:image/{0};base64,{1}", extensao, img);
            }

            return img;
        }



        public static ImageUtilsObj Base64ToDrawingImage(string base64)
        {
            var img = new ImageUtilsObj();

            if (!String.IsNullOrEmpty(base64))
            {
                try
                {
                    var base64Src = base64;
                    if (base64Src.ToLower().StartsWith("data:"))
                        base64Src = base64.Substring(base64.IndexOf(',') + 1);

                    if (!String.IsNullOrEmpty(base64Src))
                    {
                        byte[] bytes = Utils.GetImageBufferFromString(base64Src);

                        try
                        {
                            using (var ms = new MemoryStream(bytes))
                            {
                                img.ImagemDrawing = Image.FromStream(ms);
                                img.Length = ms.Length;
                            }
                        }
                        catch
                        {
                            var str = new MemoryStream();

                            /*According to these articles: http://www.akadia.com/services/dotnet_load_blob.html and http://www.eggheadcafe.com/articles/20050911.asp "Northwind Employees table were designed from MS Access, which expects a 78 byte OLE header". 
                             * You have to omit the first 78 from the MemoryStream.*/
                            int offset = 78;
                            str.Write(bytes, offset, bytes.Length - offset);
                            var im = Image.FromStream(str);

                            img.ImagemDrawing = Image.FromStream(str);
                            img.Length = str.Length;
                        }
                    }
                }
                catch { }
            }

            return img;
        }

    }
}
