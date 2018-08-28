using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace Framework
{
    public class Utils
    {
        public static string FullConvertDateToISO8601(string date)
        {
            try
            {
                return String.IsNullOrEmpty(date) || String.IsNullOrWhiteSpace(date) ? 
                    "" :
                    ConvertDateToISO8601(ConvertStringToDate(date));
            }
            catch
            {
                return "";
            }
        }
		
        /// <summary>
        /// Converte uma string com uma data completa para datetime
        /// </summary>
        /// <param name="data">data completa</param>
        /// <returns>data completa em formato datetime</returns>
        public static DateTime ConvertStringToDate(string data)
        {
            DateTimeFormatInfo formatoData = new DateTimeFormatInfo { ShortDatePattern = "dd-MM-yyyy HH:mm:ss" };

            try
            {
                return DateTime.Parse(data, formatoData);
            }
            catch
            {
                return DateTime.Parse("01-01-1900 00:00:00", formatoData);
            }
        }

        /// <summary>
        /// Converte uma string com uma data por extenso para datetime
        /// </summary>
        /// <param name="data">data completa</param>
        /// <returns>data completa em formato datetime</returns>
        public static DateTime ConvertStringToDateExtended(string data)
        {
            DateTimeFormatInfo formatoData = new DateTimeFormatInfo { ShortDatePattern = "dd-MM-yyyy HH:mm:ss" };

            if(String.IsNullOrEmpty(data))
                return DateTime.Parse("01-01-1900 00:00:00", formatoData);

            
            try
            {
                return DateTime.Parse(data, formatoData);
            }
            catch
            {
                try
                {
                    data = data.Replace(",", "");
                    return DateTime.ParseExact(data, "ddd MMM dd yyyy HH:mm:ss 'GMT'K '(GMT Standard Time)'", CultureInfo.InvariantCulture);
                }
                catch
                {
                    return DateTime.Parse("01-01-1900 00:00:00", formatoData);
                }
            }
        }

        /// <summary>
        /// Converte uma string com uma data completa para datetime
        /// </summary>
        /// <param name="data">data completa</param>
        /// <returns>data completa em formato datetime
        /// Caso a string esteja nula,vazia ou seja espaço em branco, devolve null</returns>
        public static DateTime? ConvertStringToNullableDate(string data)
        {
            DateTimeFormatInfo formatoData = new DateTimeFormatInfo { ShortDatePattern = "dd-MM-yyyy HH:mm:ss" };

            try
            {
                return string.IsNullOrWhiteSpace(data) ? (DateTime?)null : DateTime.Parse(data, formatoData);
            }
            catch
            {
                return (DateTime?)null;
            }
        }

        /// <summary>
        /// Converte uma string com uma data completa para datetime
        /// Se a string estiver vazia devolve DateTime.MinValue
        /// </summary>
        /// <param name="data">data completa</param>
        /// <returns>data completa em formato datetime</returns>
        public static DateTime ConvertStringToMinimumDate(string data)
        {
            DateTimeFormatInfo formatoData = new DateTimeFormatInfo { ShortDatePattern = "dd-MM-yyyy HH:mm:ss" };

            try
            {
                return string.IsNullOrWhiteSpace(data) ? DateTime.MinValue : DateTime.Parse(data, formatoData);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Converte uma string com uma data completa para datetime
        /// Se a string estiver vazia devolve DateTime.MaxValue
        /// </summary>
        /// <param name="data">data completa</param>
        /// <returns>data completa em formato datetime</returns>
        public static DateTime ConvertStringToMaximumDate(string data)
        {
            DateTimeFormatInfo formatoData = new DateTimeFormatInfo { ShortDatePattern = "dd-MM-yyyy HH:mm:ss" };

            try
            {
                return string.IsNullOrWhiteSpace(data) ? DateTime.MaxValue : DateTime.Parse(data, formatoData);
            }
            catch
            {
                return DateTime.MaxValue;
            }
        }

        /// <summary>
        /// Converte uma data completa em formato string
        /// </summary>
        /// <param name="data">Data completa</param>
        /// <returns>data completa em formato string</returns>
        public static string ConvertDateToString(DateTime data)
        {
            if (data == DateTime.MinValue || data == DateTime.MaxValue)
                return "";
            return data.ToString("dd-MM-yyyy");
        }

        /// <summary>
        /// Converte uma data completa em formato string
        /// </summary>
        /// <param name="data">Data completa</param>
        /// <returns>data completa em formato string</returns>
        public static string ConvertDateToString(DateTime? data)
        {
            if (data.HasValue)
                return ConvertDateToString(data.Value);
            else
                return "";
        }

        /// <summary>
        /// Converte uma data completa em formato string
        /// </summary>
        /// <param name="data">Data completa</param>
        /// <returns>data completa em formato string</returns>
        public static string ConvertDateTimeToString(DateTime data)
        {
            if (data == DateTime.MinValue || data == DateTime.MaxValue)
                return "";
            return data.ToString("dd-MM-yyyy HH:mm:ss");
        }

        /// <summary>
        /// Converte uma data completa em formato string
        /// </summary>
        /// <param name="data">Data completa</param>
        /// <returns>data completa em formato string</returns>
        public static string ConvertDateTimeToString(DateTime? data)
        {
            if (data.HasValue)
                return ConvertDateTimeToString(data.Value);
            else
                return "";
        }


        /// <summary>
        /// Converte uma data completa em formato string
        /// </summary>
        /// <param name="data">Data completa</param>
        /// <returns>data completa em formato string</returns>
        public static string ConvertDateToISO8601(DateTime data)
        {
            if (data == DateTime.MinValue || data == DateTime.MaxValue)
                return "";
            return data.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converte uma data completa em formato string
        /// </summary>
        /// <param name="data">Data completa</param>
        /// <returns>data completa em formato string</returns>
        public static string ConvertDateToISO8601(DateTime? data)
        {
            if (data.HasValue)
                return ConvertDateToISO8601(data.Value);
            else
                return "";
        }

        /// <summary>
        /// Converte a parte horária de uma data em formato string
        /// </summary>
        /// <param name="data">Data completa</param>
        /// <returns>parte horária em formato string</returns>
        public static string ConvertHourToString(DateTime data)
        {
            return data.ToString("HH:mm");
        }

        /// <summary>
        /// Converte a parte horária de uma data em formato string
        /// </summary>
        /// <param name="data">Data completa</param>
        /// <returns>parte horária em formato string</returns>
        public static string ConvertHourToString(DateTime? data)
        {
            if (data.HasValue)
                return data.Value.ToString("HH:mm");
            return "";
        }

        /// <summary>
        /// Converte uma string com a parte horária num datetime
        /// </summary>
        /// <param name="horas">parte horária</param>
        /// <returns>parte horária em datetime</returns>
        public static DateTime ConvertStringToHour(string horas)
        {
            DateTime hora = new DateTime();

            if (!DateTime.TryParseExact(horas, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out hora))
                hora = new DateTime();

            return hora;
        }

        /// <summary>
        /// Converte uma string com a parte horária num datetime
        /// </summary>
        /// <param name="horas">parte horária</param>
        /// <returns>parte horária em datetime</returns>
        public static DateTime? ConvertStringToNullableHour(string horas)
        {
            DateTime hora = new DateTime();

            try
            {
                if (string.IsNullOrWhiteSpace(horas) || string.IsNullOrEmpty(horas))
                    return (DateTime?)null;
                if (!DateTime.TryParseExact(horas, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out hora))
                {
                    var hour = 0; var minutes = 0;

                    //horas
                    var reg = new Regex(@"([01]?[0-9]|2[0-3])h");
                    var m = reg.Match(horas.ToLower());

                    if (m.Success)
                        hour = int.Parse(m.Value.Replace("h",""));

                    //minutes
                    reg = new Regex(@"[0-5][0-9]m");
                    m = reg.Match(horas.ToLower());

                    if (m.Success)
                        minutes = int.Parse(m.Value.Replace("h", ""));


                    hora = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minutes, 0);
                }
            }
            catch
            {}

            return hora;
        }

        /// <summary>
        /// Converte um valor decimal em formato string
        /// </summary>
        /// <param name="convert">VAlor decimal a converter</param>
        /// <returns>Valor decimal em formato string</returns>
        public static string ConvertDecimalToString(decimal convert)
        {
            return convert.ToString("0.00");
        }

        /// <summary>
        /// Converte um valor decimal nullable em formato string
        /// </summary>
        /// <param name="convert">VAlor decimal a converter</param>
        /// <returns>Valor decimal em formato string
        /// Se for nullable devolver string vazia</returns>
        public static string ConvertDecimalToString(decimal? convert)
        {
            string value = "";
            if (convert.HasValue)
            {
                try
                {
                    value = convert.Value.ToString("0.00");
                }
                catch { }
            } return value;
        }

        /// <summary>
        /// Converte um valor decimal do formato string para decimal
        /// </summary>
        /// <param name="convert">valor decimal a converter</param>
        /// <returns>valor em formato decimal</returns>
        public static decimal? ConvertStringToDecimal(string convert)
        {
            decimal retorno = 0;
            try
            {
                if (string.IsNullOrWhiteSpace(convert))
                    return (decimal?)null;
                decimal.TryParse(convert.Replace(".", ","), out retorno);
                return retorno;
            }
            catch { return (decimal?)null; }
        }

        /// <summary>
        /// Converte um valor decimal para um inteiro em formato string
        /// </summary>
        /// <param name="convert">valor decimal a converter</param>
        /// <returns>valor inteiro em formato string</returns>
        public static string ConvertDecimalToIntString(decimal convert)
        {
            return convert.ToString("0");
        }

        /// <summary>
        /// Converte um valor decimal para um inteiro em formato string
        /// Se o valor a converter for null, devolve ""
        /// </summary>
        /// <param name="convert">valor decimal a converter</param>
        /// <returns>valor inteiro em formato string</returns>
        public static string ConvertDecimalToIntString(decimal? convert)
        {
            if (convert.HasValue)
                return convert.Value.ToString("0");
            return "";
        }

        /// <summary>
        /// Retorna uma string da duração (em formato HH:mm) do evento com a data de inicio e, se tiver, a data de fim
        /// </summary>
        /// <param name="duracaoInicio">Data de inicio</param>
        /// <param name="duracaoFim">Data de fim</param>
        /// <returns></returns>
        public static string ReturnEventTime(DateTime? duracaoInicio, DateTime? duracaoFim)
        {
            StringBuilder tempo = new StringBuilder();
            if (duracaoInicio.HasValue)
            {
                tempo.Append(Utils.ConvertHourToString(duracaoInicio.Value));
                if (duracaoFim.HasValue)
                    tempo.Append(" - " + Utils.ConvertHourToString(duracaoFim.Value));
            }
            return tempo.ToString();
        }

        /// <summary>
        /// Retorna uma string composta pela data inicio e, se tiver preenchida, a data de fim
        /// dd-MM-yyyy - dd-MM-yyyy
        /// </summary>
        /// <param name="dataInicio">Data de inicio</param>
        /// <param name="dataFim">Data de fim</param>
        /// <returns>String com a composição da data</returns>
        public static string ReturnFullDate(DateTime dataInicio, DateTime? dataFim)
        {
            StringBuilder tempo = new StringBuilder();
            tempo.Append(ConvertDateToString(dataInicio));
            if (dataFim.HasValue)
            {
                tempo.Append(" - " + Utils.ConvertHourToString(dataFim.Value));
            }
            return tempo.ToString();
        }

        /// <summary>
        /// Obtem número da semana de determinada data
        /// </summary>
        /// <param name="data">data completa</param>
        /// <returns>número da semana</returns>
        public static int GetDateWeek(DateTime data)
        {
            var dfi = DateTimeFormatInfo.CurrentInfo;
            var date1 = new DateTime(2011, 1, 1);
            System.Globalization.Calendar cal = dfi.Calendar;

            return cal.GetWeekOfYear(data, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }

        /// <summary>
        /// Obtem o MIME type de um ficheiro
        /// </summary>
        /// <param name="fileName">Nome do ficheiro</param>
        /// <returns>string com o MIME type</returns>
        public static string GetMimeType(string fileName)
        {
            string extension = System.IO.Path.GetExtension(fileName).ToLower();
            if (extension == null)
            {
                throw new ArgumentNullException("extension");
            }

            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }

            string mimeType = "application/unknown";

            if (!_mimeTypes.TryGetValue(extension, out mimeType))
            {
                string ext = System.IO.Path.GetExtension(fileName).ToLower();
                Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
                if (regKey != null && regKey.GetValue("Content Type") != null)
                    mimeType = regKey.GetValue("Content Type").ToString();
            }
            return mimeType;
        }

        private static IDictionary<string, string> _mimeTypes = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {
        #region Big freaking list of mime types
        // combination of values from Windows 7 Registry and 
        // from C:\Windows\System32\inetsrv\config\applicationHost.config
        // some added, including .7z and .dat
        {".323", "text/h323"},
        {".3g2", "video/3gpp2"},
        {".3gp", "video/3gpp"},
        {".3gp2", "video/3gpp2"},
        {".3gpp", "video/3gpp"},
        {".7z", "application/x-7z-compressed"},
        {".aa", "audio/audible"},
        {".AAC", "audio/aac"},
        {".aaf", "application/octet-stream"},
        {".aax", "audio/vnd.audible.aax"},
        {".ac3", "audio/ac3"},
        {".aca", "application/octet-stream"},
        {".accda", "application/msaccess.addin"},
        {".accdb", "application/msaccess"},
        {".accdc", "application/msaccess.cab"},
        {".accde", "application/msaccess"},
        {".accdr", "application/msaccess.runtime"},
        {".accdt", "application/msaccess"},
        {".accdw", "application/msaccess.webapplication"},
        {".accft", "application/msaccess.ftemplate"},
        {".acx", "application/internet-property-stream"},
        {".AddIn", "text/xml"},
        {".ade", "application/msaccess"},
        {".adobebridge", "application/x-bridge-url"},
        {".adp", "application/msaccess"},
        {".ADT", "audio/vnd.dlna.adts"},
        {".ADTS", "audio/aac"},
        {".afm", "application/octet-stream"},
        {".ai", "application/postscript"},
        {".aif", "audio/x-aiff"},
        {".aifc", "audio/aiff"},
        {".aiff", "audio/aiff"},
        {".air", "application/vnd.adobe.air-application-installer-package+zip"},
        {".amc", "application/x-mpeg"},
        {".application", "application/x-ms-application"},
        {".art", "image/x-jg"},
        {".asa", "application/xml"},
        {".asax", "application/xml"},
        {".ascx", "application/xml"},
        {".asd", "application/octet-stream"},
        {".asf", "video/x-ms-asf"},
        {".ashx", "application/xml"},
        {".asi", "application/octet-stream"},
        {".asm", "text/plain"},
        {".asmx", "application/xml"},
        {".aspx", "application/xml"},
        {".asr", "video/x-ms-asf"},
        {".asx", "video/x-ms-asf"},
        {".atom", "application/atom+xml"},
        {".au", "audio/basic"},
        {".avi", "video/x-msvideo"},
        {".axs", "application/olescript"},
        {".bas", "text/plain"},
        {".bcpio", "application/x-bcpio"},
        {".bin", "application/octet-stream"},
        {".bmp", "image/bmp"},
        {".c", "text/plain"},
        {".cab", "application/octet-stream"},
        {".caf", "audio/x-caf"},
        {".calx", "application/vnd.ms-office.calx"},
        {".cat", "application/vnd.ms-pki.seccat"},
        {".cc", "text/plain"},
        {".cd", "text/plain"},
        {".cdda", "audio/aiff"},
        {".cdf", "application/x-cdf"},
        {".cer", "application/x-x509-ca-cert"},
        {".chm", "application/octet-stream"},
        {".class", "application/x-java-applet"},
        {".clp", "application/x-msclip"},
        {".cmx", "image/x-cmx"},
        {".cnf", "text/plain"},
        {".cod", "image/cis-cod"},
        {".config", "application/xml"},
        {".contact", "text/x-ms-contact"},
        {".coverage", "application/xml"},
        {".cpio", "application/x-cpio"},
        {".cpp", "text/plain"},
        {".crd", "application/x-mscardfile"},
        {".crl", "application/pkix-crl"},
        {".crt", "application/x-x509-ca-cert"},
        {".cs", "text/plain"},
        {".csdproj", "text/plain"},
        {".csh", "application/x-csh"},
        {".csproj", "text/plain"},
        {".css", "text/css"},
        {".csv", "text/csv"},
        {".cur", "application/octet-stream"},
        {".cxx", "text/plain"},
        {".dat", "application/octet-stream"},
        {".datasource", "application/xml"},
        {".dbproj", "text/plain"},
        {".dcr", "application/x-director"},
        {".def", "text/plain"},
        {".deploy", "application/octet-stream"},
        {".der", "application/x-x509-ca-cert"},
        {".dgml", "application/xml"},
        {".dib", "image/bmp"},
        {".dif", "video/x-dv"},
        {".dir", "application/x-director"},
        {".disco", "text/xml"},
        {".dll", "application/x-msdownload"},
        {".dll.config", "text/xml"},
        {".dlm", "text/dlm"},
        {".doc", "application/msword"},
        {".docm", "application/vnd.ms-word.document.macroEnabled.12"},
        {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
        {".dot", "application/msword"},
        {".dotm", "application/vnd.ms-word.template.macroEnabled.12"},
        {".dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
        {".dsp", "application/octet-stream"},
        {".dsw", "text/plain"},
        {".dtd", "text/xml"},
        {".dtsConfig", "text/xml"},
        {".dv", "video/x-dv"},
        {".dvi", "application/x-dvi"},
        {".dwf", "drawing/x-dwf"},
        {".dwp", "application/octet-stream"},
        {".dxr", "application/x-director"},
        {".eml", "message/rfc822"},
        {".emz", "application/octet-stream"},
        {".eot", "application/octet-stream"},
        {".eps", "application/postscript"},
        {".etl", "application/etl"},
        {".etx", "text/x-setext"},
        {".evy", "application/envoy"},
        {".exe", "application/octet-stream"},
        {".exe.config", "text/xml"},
        {".fdf", "application/vnd.fdf"},
        {".fif", "application/fractals"},
        {".filters", "Application/xml"},
        {".fla", "application/octet-stream"},
        {".flr", "x-world/x-vrml"},
        {".flv", "video/x-flv"},
        {".fsscript", "application/fsharp-script"},
        {".fsx", "application/fsharp-script"},
        {".generictest", "application/xml"},
        {".gif", "image/gif"},
        {".group", "text/x-ms-group"},
        {".gsm", "audio/x-gsm"},
        {".gtar", "application/x-gtar"},
        {".gz", "application/x-gzip"},
        {".h", "text/plain"},
        {".hdf", "application/x-hdf"},
        {".hdml", "text/x-hdml"},
        {".hhc", "application/x-oleobject"},
        {".hhk", "application/octet-stream"},
        {".hhp", "application/octet-stream"},
        {".hlp", "application/winhlp"},
        {".hpp", "text/plain"},
        {".hqx", "application/mac-binhex40"},
        {".hta", "application/hta"},
        {".htc", "text/x-component"},
        {".htm", "text/html"},
        {".html", "text/html"},
        {".htt", "text/webviewhtml"},
        {".hxa", "application/xml"},
        {".hxc", "application/xml"},
        {".hxd", "application/octet-stream"},
        {".hxe", "application/xml"},
        {".hxf", "application/xml"},
        {".hxh", "application/octet-stream"},
        {".hxi", "application/octet-stream"},
        {".hxk", "application/xml"},
        {".hxq", "application/octet-stream"},
        {".hxr", "application/octet-stream"},
        {".hxs", "application/octet-stream"},
        {".hxt", "text/html"},
        {".hxv", "application/xml"},
        {".hxw", "application/octet-stream"},
        {".hxx", "text/plain"},
        {".i", "text/plain"},
        {".ico", "image/x-icon"},
        {".ics", "application/octet-stream"},
        {".idl", "text/plain"},
        {".ief", "image/ief"},
        {".iii", "application/x-iphone"},
        {".inc", "text/plain"},
        {".inf", "application/octet-stream"},
        {".inl", "text/plain"},
        {".ins", "application/x-internet-signup"},
        {".ipa", "application/x-itunes-ipa"},
        {".ipg", "application/x-itunes-ipg"},
        {".ipproj", "text/plain"},
        {".ipsw", "application/x-itunes-ipsw"},
        {".iqy", "text/x-ms-iqy"},
        {".isp", "application/x-internet-signup"},
        {".ite", "application/x-itunes-ite"},
        {".itlp", "application/x-itunes-itlp"},
        {".itms", "application/x-itunes-itms"},
        {".itpc", "application/x-itunes-itpc"},
        {".IVF", "video/x-ivf"},
        {".jar", "application/java-archive"},
        {".java", "application/octet-stream"},
        {".jck", "application/liquidmotion"},
        {".jcz", "application/liquidmotion"},
        {".jfif", "image/pjpeg"},
        {".jnlp", "application/x-java-jnlp-file"},
        {".jpb", "application/octet-stream"},
        {".jpe", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".jpg", "image/jpeg"},
        {".js", "application/x-javascript"},
        {".json", "application/json"},
        {".jsx", "text/jscript"},
        {".jsxbin", "text/plain"},
        {".latex", "application/x-latex"},
        {".library-ms", "application/windows-library+xml"},
        {".lit", "application/x-ms-reader"},
        {".loadtest", "application/xml"},
        {".lpk", "application/octet-stream"},
        {".lsf", "video/x-la-asf"},
        {".lst", "text/plain"},
        {".lsx", "video/x-la-asf"},
        {".lzh", "application/octet-stream"},
        {".m13", "application/x-msmediaview"},
        {".m14", "application/x-msmediaview"},
        {".m1v", "video/mpeg"},
        {".m2t", "video/vnd.dlna.mpeg-tts"},
        {".m2ts", "video/vnd.dlna.mpeg-tts"},
        {".m2v", "video/mpeg"},
        {".m3u", "audio/x-mpegurl"},
        {".m3u8", "audio/x-mpegurl"},
        {".m4a", "audio/m4a"},
        {".m4b", "audio/m4b"},
        {".m4p", "audio/m4p"},
        {".m4r", "audio/x-m4r"},
        {".m4v", "video/x-m4v"},
        {".mac", "image/x-macpaint"},
        {".mak", "text/plain"},
        {".man", "application/x-troff-man"},
        {".manifest", "application/x-ms-manifest"},
        {".map", "text/plain"},
        {".master", "application/xml"},
        {".mda", "application/msaccess"},
        {".mdb", "application/x-msaccess"},
        {".mde", "application/msaccess"},
        {".mdp", "application/octet-stream"},
        {".me", "application/x-troff-me"},
        {".mfp", "application/x-shockwave-flash"},
        {".mht", "message/rfc822"},
        {".mhtml", "message/rfc822"},
        {".mid", "audio/mid"},
        {".midi", "audio/mid"},
        {".mix", "application/octet-stream"},
        {".mk", "text/plain"},
        {".mmf", "application/x-smaf"},
        {".mno", "text/xml"},
        {".mny", "application/x-msmoney"},
        {".mod", "video/mpeg"},
        {".mov", "video/quicktime"},
        {".movie", "video/x-sgi-movie"},
        {".mp2", "video/mpeg"},
        {".mp2v", "video/mpeg"},
        {".mp3", "audio/mpeg"},
        {".mp4", "video/mp4"},
        {".mp4v", "video/mp4"},
        {".mpa", "video/mpeg"},
        {".mpe", "video/mpeg"},
        {".mpeg", "video/mpeg"},
        {".mpf", "application/vnd.ms-mediapackage"},
        {".mpg", "video/mpeg"},
        {".mpp", "application/vnd.ms-project"},
        {".mpv2", "video/mpeg"},
        {".mqv", "video/quicktime"},
        {".ms", "application/x-troff-ms"},
        {".msi", "application/octet-stream"},
        {".mso", "application/octet-stream"},
        {".mts", "video/vnd.dlna.mpeg-tts"},
        {".mtx", "application/xml"},
        {".mvb", "application/x-msmediaview"},
        {".mvc", "application/x-miva-compiled"},
        {".mxp", "application/x-mmxp"},
        {".nc", "application/x-netcdf"},
        {".nsc", "video/x-ms-asf"},
        {".nws", "message/rfc822"},
        {".ocx", "application/octet-stream"},
        {".oda", "application/oda"},
        {".odc", "text/x-ms-odc"},
        {".odh", "text/plain"},
        {".odl", "text/plain"},
        {".odp", "application/vnd.oasis.opendocument.presentation"},
        {".ods", "application/oleobject"},
        {".odt", "application/vnd.oasis.opendocument.text"},
        {".one", "application/onenote"},
        {".onea", "application/onenote"},
        {".onepkg", "application/onenote"},
        {".onetmp", "application/onenote"},
        {".onetoc", "application/onenote"},
        {".onetoc2", "application/onenote"},
        {".orderedtest", "application/xml"},
        {".osdx", "application/opensearchdescription+xml"},
        {".p10", "application/pkcs10"},
        {".p12", "application/x-pkcs12"},
        {".p7b", "application/x-pkcs7-certificates"},
        {".p7c", "application/pkcs7-mime"},
        {".p7m", "application/pkcs7-mime"},
        {".p7r", "application/x-pkcs7-certreqresp"},
        {".p7s", "application/pkcs7-signature"},
        {".pbm", "image/x-portable-bitmap"},
        {".pcast", "application/x-podcast"},
        {".pct", "image/pict"},
        {".pcx", "application/octet-stream"},
        {".pcz", "application/octet-stream"},
        {".pdf", "application/pdf"},
        {".pfb", "application/octet-stream"},
        {".pfm", "application/octet-stream"},
        {".pfx", "application/x-pkcs12"},
        {".pgm", "image/x-portable-graymap"},
        {".pic", "image/pict"},
        {".pict", "image/pict"},
        {".pkgdef", "text/plain"},
        {".pkgundef", "text/plain"},
        {".pko", "application/vnd.ms-pki.pko"},
        {".pls", "audio/scpls"},
        {".pma", "application/x-perfmon"},
        {".pmc", "application/x-perfmon"},
        {".pml", "application/x-perfmon"},
        {".pmr", "application/x-perfmon"},
        {".pmw", "application/x-perfmon"},
        {".png", "image/png"},
        {".pnm", "image/x-portable-anymap"},
        {".pnt", "image/x-macpaint"},
        {".pntg", "image/x-macpaint"},
        {".pnz", "image/png"},
        {".pot", "application/vnd.ms-powerpoint"},
        {".potm", "application/vnd.ms-powerpoint.template.macroEnabled.12"},
        {".potx", "application/vnd.openxmlformats-officedocument.presentationml.template"},
        {".ppa", "application/vnd.ms-powerpoint"},
        {".ppam", "application/vnd.ms-powerpoint.addin.macroEnabled.12"},
        {".ppm", "image/x-portable-pixmap"},
        {".pps", "application/vnd.ms-powerpoint"},
        {".ppsm", "application/vnd.ms-powerpoint.slideshow.macroEnabled.12"},
        {".ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
        {".ppt", "application/vnd.ms-powerpoint"},
        {".pptm", "application/vnd.ms-powerpoint.presentation.macroEnabled.12"},
        {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
        {".prf", "application/pics-rules"},
        {".prm", "application/octet-stream"},
        {".prx", "application/octet-stream"},
        {".ps", "application/postscript"},
        {".psc1", "application/PowerShell"},
        {".psd", "application/octet-stream"},
        {".psess", "application/xml"},
        {".psm", "application/octet-stream"},
        {".psp", "application/octet-stream"},
        {".pub", "application/x-mspublisher"},
        {".pwz", "application/vnd.ms-powerpoint"},
        {".qht", "text/x-html-insertion"},
        {".qhtm", "text/x-html-insertion"},
        {".qt", "video/quicktime"},
        {".qti", "image/x-quicktime"},
        {".qtif", "image/x-quicktime"},
        {".qtl", "application/x-quicktimeplayer"},
        {".qxd", "application/octet-stream"},
        {".ra", "audio/x-pn-realaudio"},
        {".ram", "audio/x-pn-realaudio"},
        {".rar", "application/octet-stream"},
        {".ras", "image/x-cmu-raster"},
        {".rat", "application/rat-file"},
        {".rc", "text/plain"},
        {".rc2", "text/plain"},
        {".rct", "text/plain"},
        {".rdlc", "application/xml"},
        {".resx", "application/xml"},
        {".rf", "image/vnd.rn-realflash"},
        {".rgb", "image/x-rgb"},
        {".rgs", "text/plain"},
        {".rm", "application/vnd.rn-realmedia"},
        {".rmi", "audio/mid"},
        {".rmp", "application/vnd.rn-rn_music_package"},
        {".roff", "application/x-troff"},
        {".rpm", "audio/x-pn-realaudio-plugin"},
        {".rqy", "text/x-ms-rqy"},
        {".rtf", "application/rtf"},
        {".rtx", "text/richtext"},
        {".ruleset", "application/xml"},
        {".s", "text/plain"},
        {".safariextz", "application/x-safari-safariextz"},
        {".scd", "application/x-msschedule"},
        {".sct", "text/scriptlet"},
        {".sd2", "audio/x-sd2"},
        {".sdp", "application/sdp"},
        {".sea", "application/octet-stream"},
        {".searchConnector-ms", "application/windows-search-connector+xml"},
        {".setpay", "application/set-payment-initiation"},
        {".setreg", "application/set-registration-initiation"},
        {".settings", "application/xml"},
        {".sgimb", "application/x-sgimb"},
        {".sgml", "text/sgml"},
        {".sh", "application/x-sh"},
        {".shar", "application/x-shar"},
        {".shtml", "text/html"},
        {".sit", "application/x-stuffit"},
        {".sitemap", "application/xml"},
        {".skin", "application/xml"},
        {".sldm", "application/vnd.ms-powerpoint.slide.macroEnabled.12"},
        {".sldx", "application/vnd.openxmlformats-officedocument.presentationml.slide"},
        {".slk", "application/vnd.ms-excel"},
        {".sln", "text/plain"},
        {".slupkg-ms", "application/x-ms-license"},
        {".smd", "audio/x-smd"},
        {".smi", "application/octet-stream"},
        {".smx", "audio/x-smd"},
        {".smz", "audio/x-smd"},
        {".snd", "audio/basic"},
        {".snippet", "application/xml"},
        {".snp", "application/octet-stream"},
        {".sol", "text/plain"},
        {".sor", "text/plain"},
        {".spc", "application/x-pkcs7-certificates"},
        {".spl", "application/futuresplash"},
        {".src", "application/x-wais-source"},
        {".srf", "text/plain"},
        {".SSISDeploymentManifest", "text/xml"},
        {".ssm", "application/streamingmedia"},
        {".sst", "application/vnd.ms-pki.certstore"},
        {".stl", "application/vnd.ms-pki.stl"},
        {".sv4cpio", "application/x-sv4cpio"},
        {".sv4crc", "application/x-sv4crc"},
        {".svc", "application/xml"},
        {".swf", "application/x-shockwave-flash"},
        {".t", "application/x-troff"},
        {".tar", "application/x-tar"},
        {".tcl", "application/x-tcl"},
        {".testrunconfig", "application/xml"},
        {".testsettings", "application/xml"},
        {".tex", "application/x-tex"},
        {".texi", "application/x-texinfo"},
        {".texinfo", "application/x-texinfo"},
        {".tgz", "application/x-compressed"},
        {".thmx", "application/vnd.ms-officetheme"},
        {".thn", "application/octet-stream"},
        {".tif", "image/tiff"},
        {".tiff", "image/tiff"},
        {".tlh", "text/plain"},
        {".tli", "text/plain"},
        {".toc", "application/octet-stream"},
        {".tr", "application/x-troff"},
        {".trm", "application/x-msterminal"},
        {".trx", "application/xml"},
        {".ts", "video/vnd.dlna.mpeg-tts"},
        {".tsv", "text/tab-separated-values"},
        {".ttf", "application/octet-stream"},
        {".tts", "video/vnd.dlna.mpeg-tts"},
        {".txt", "text/plain"},
        {".u32", "application/octet-stream"},
        {".uls", "text/iuls"},
        {".user", "text/plain"},
        {".ustar", "application/x-ustar"},
        {".vb", "text/plain"},
        {".vbdproj", "text/plain"},
        {".vbk", "video/mpeg"},
        {".vbproj", "text/plain"},
        {".vbs", "text/vbscript"},
        {".vcf", "text/x-vcard"},
        {".vcproj", "Application/xml"},
        {".vcs", "text/plain"},
        {".vcxproj", "Application/xml"},
        {".vddproj", "text/plain"},
        {".vdp", "text/plain"},
        {".vdproj", "text/plain"},
        {".vdx", "application/vnd.ms-visio.viewer"},
        {".vml", "text/xml"},
        {".vscontent", "application/xml"},
        {".vsct", "text/xml"},
        {".vsd", "application/vnd.visio"},
        {".vsi", "application/ms-vsi"},
        {".vsix", "application/vsix"},
        {".vsixlangpack", "text/xml"},
        {".vsixmanifest", "text/xml"},
        {".vsmdi", "application/xml"},
        {".vspscc", "text/plain"},
        {".vss", "application/vnd.visio"},
        {".vsscc", "text/plain"},
        {".vssettings", "text/xml"},
        {".vssscc", "text/plain"},
        {".vst", "application/vnd.visio"},
        {".vstemplate", "text/xml"},
        {".vsto", "application/x-ms-vsto"},
        {".vsw", "application/vnd.visio"},
        {".vsx", "application/vnd.visio"},
        {".vtx", "application/vnd.visio"},
        {".wav", "audio/wav"},
        {".wave", "audio/wav"},
        {".wax", "audio/x-ms-wax"},
        {".wbk", "application/msword"},
        {".wbmp", "image/vnd.wap.wbmp"},
        {".wcm", "application/vnd.ms-works"},
        {".wdb", "application/vnd.ms-works"},
        {".wdp", "image/vnd.ms-photo"},
        {".webarchive", "application/x-safari-webarchive"},
        {".webtest", "application/xml"},
        {".wiq", "application/xml"},
        {".wiz", "application/msword"},
        {".wks", "application/vnd.ms-works"},
        {".WLMP", "application/wlmoviemaker"},
        {".wlpginstall", "application/x-wlpg-detect"},
        {".wlpginstall3", "application/x-wlpg3-detect"},
        {".wm", "video/x-ms-wm"},
        {".wma", "audio/x-ms-wma"},
        {".wmd", "application/x-ms-wmd"},
        {".wmf", "application/x-msmetafile"},
        {".wml", "text/vnd.wap.wml"},
        {".wmlc", "application/vnd.wap.wmlc"},
        {".wmls", "text/vnd.wap.wmlscript"},
        {".wmlsc", "application/vnd.wap.wmlscriptc"},
        {".wmp", "video/x-ms-wmp"},
        {".wmv", "video/x-ms-wmv"},
        {".wmx", "video/x-ms-wmx"},
        {".wmz", "application/x-ms-wmz"},
        {".wpl", "application/vnd.ms-wpl"},
        {".wps", "application/vnd.ms-works"},
        {".wri", "application/x-mswrite"},
        {".wrl", "x-world/x-vrml"},
        {".wrz", "x-world/x-vrml"},
        {".wsc", "text/scriptlet"},
        {".wsdl", "text/xml"},
        {".wvx", "video/x-ms-wvx"},
        {".x", "application/directx"},
        {".xaf", "x-world/x-vrml"},
        {".xaml", "application/xaml+xml"},
        {".xap", "application/x-silverlight-app"},
        {".xbap", "application/x-ms-xbap"},
        {".xbm", "image/x-xbitmap"},
        {".xdr", "text/plain"},
        {".xht", "application/xhtml+xml"},
        {".xhtml", "application/xhtml+xml"},
        {".xla", "application/vnd.ms-excel"},
        {".xlam", "application/vnd.ms-excel.addin.macroEnabled.12"},
        {".xlc", "application/vnd.ms-excel"},
        {".xld", "application/vnd.ms-excel"},
        {".xlk", "application/vnd.ms-excel"},
        {".xll", "application/vnd.ms-excel"},
        {".xlm", "application/vnd.ms-excel"},
        {".xls", "application/vnd.ms-excel"},
        {".xlsb", "application/vnd.ms-excel.sheet.binary.macroEnabled.12"},
        {".xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12"},
        {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
        {".xlt", "application/vnd.ms-excel"},
        {".xltm", "application/vnd.ms-excel.template.macroEnabled.12"},
        {".xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
        {".xlw", "application/vnd.ms-excel"},
        {".xml", "text/xml"},
        {".xmta", "application/xml"},
        {".xof", "x-world/x-vrml"},
        {".XOML", "text/plain"},
        {".xpm", "image/x-xpixmap"},
        {".xps", "application/vnd.ms-xpsdocument"},
        {".xrm-ms", "text/xml"},
        {".xsc", "application/xml"},
        {".xsd", "text/xml"},
        {".xsf", "text/xml"},
        {".xsl", "text/xml"},
        {".xslt", "text/xml"},
        {".xsn", "application/octet-stream"},
        {".xss", "application/xml"},
        {".xtp", "application/octet-stream"},
        {".xwd", "image/x-xwindowdump"},
        {".z", "application/x-compress"},
        {".zip", "application/x-zip-compressed"},
        #endregion
        };

        public static bool IsImageFile(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
                return false;

            var mimeType = GetMimeType(fileName);

            return mimeType != null && mimeType.Contains('/') && mimeType.Substring(0, mimeType.IndexOf('/')) == "image";
        }
        public static bool IsVideoFile(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
                return false;

            var mimeType = GetMimeType(fileName);

            return mimeType != null && mimeType.Contains('/') && mimeType.Substring(0, mimeType.IndexOf('/')) == "video";
        }
        public static bool IsAudioFile(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
                return false;

            var mimeType = GetMimeType(fileName);

            return mimeType != null && mimeType.Contains('/') && mimeType.Substring(0, mimeType.IndexOf('/')) == "audio";
        }

        /// <summary>
        /// Retorna o caminho completo para a imagem
        /// </summary>
        /// <param name="folderPath">Caminho base</param>
        /// <param name="idProject">Identificador do projecto</param>
        /// <param name="fileName">Nome do ficheiro</param>
        /// <returns>string com o caminho completo</returns>
        public static string GetFilePathToSave(string folderPath, int idProject, string fileName)
        {
            if (!Directory.Exists(folderPath + idProject.ToString()))
                Directory.CreateDirectory(folderPath + idProject.ToString());

            return string.Format("{0}{1}\\{2}", folderPath, idProject.ToString(), fileName);
        }
        public static string GetFilePath(string folderPath, string idProject, string fileName)
        {
            if (!Directory.Exists(folderPath + idProject.ToString()))
                Directory.CreateDirectory(folderPath + idProject.ToString());

            return string.Format("{0}{1}\\{2}", folderPath, idProject.ToString(), fileName);
        }
        public static string GetFilePathToUrl(string folderPath, string idProject, string fileName)
        {
            return string.Format("{0}{1}/{2}", folderPath, idProject.ToString(), fileName);
        }
        public static string GetFilePathToDownload(string path, string fileName, string idProject)
        {
            return string.Format("{0}{1}{2}", path, "?fileName=" + fileName, "&idProjecto=" + idProject);
        }

        /// <summary>
        /// Giuarda uma imagem em disco redimensionando-a, caso exceda, para as medidas passadas por parâmetro
        /// </summary>
        /// <param name="image">Imagem a guardar</param>
        /// <param name="path">Caminho onde guardar a imagem</param>
        /// <param name="width">Tamanho máximo da largura</param>
        /// <param name="height">Tamanho máximo da altura</param>
        public static void SaveImageWithResize(System.Drawing.Image image, string path, int width, int height, bool isEncoded = false, ImageCodecInfo imgCodecInfo = null, EncoderParameters encodeParameters = null)
        {
            if (image.Width != width || image.Height != height)
            {
                System.Drawing.Image resized = Utils.ResizeImage(image, new System.Drawing.Size(width, height), true);
                if (isEncoded)
                    resized.Save(path, imgCodecInfo, encodeParameters);
                else
                    resized.Save(path);
            }
            else
            {
                if (isEncoded)
                    image.Save(path, imgCodecInfo, encodeParameters);
                else
                    image.Save(path);
            }
        }
        /// <summary>
        /// Giuarda uma imagem em disco redimensionando-a, caso exceda, para as medidas passadas por parâmetro
        /// </summary>
        /// <param name="image">Imagem a guardar</param>
        /// <param name="path">Caminho onde guardar a imagem</param>
        /// <param name="width">Tamanho máximo da largura</param>
        /// <param name="height">Tamanho máximo da altura</param>
        public static void SaveImageWithResize(FileUpload image, string path, int width, int height, bool isEncoded = false, ImageCodecInfo imgCodecInfo = null, EncoderParameters encodeParameters = null)
        {
            if (width != 0 || height != 0)
            {
                System.Drawing.Image resized = Utils.ResizeImage(image, new System.Drawing.Size(width, height), true);
                if (isEncoded)
                    resized.Save(path, imgCodecInfo, encodeParameters);
                else
                    resized.Save(path);
            }
        }

        /// <summary>
        /// Recebe uma image e redimensiona-a pelo tamanho passado
        /// </summary>
        /// <param name="image">Imagem a redimensionar</param>
        /// <param name="size">Novo tamanho da imagem</param>
        /// <param name="preserveAspectRatio">Manter o racio</param>
        /// <returns>Imagem</returns>
        public static System.Drawing.Image ResizeImage(System.Drawing.Image image, Size size, bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            System.Drawing.Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        /// <summary>
        /// Recebe uma image e redimensiona-a pelo tamanho passado
        /// </summary>
        /// <param name="image">Imagem a redimensionar</param>
        /// <param name="size">Novo tamanho da imagem</param>
        /// <param name="preserveAspectRatio">Manter o racio</param>
        /// <returns>Imagem</returns>
        public static System.Drawing.Image ResizeImage(FileUpload imageOriginal, Size size, bool preserveAspectRatio = true)
        {
            int newWidth,newHeight;
            Bitmap image = new Bitmap(imageOriginal.FileContent); 
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            System.Drawing.Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        /// <summary>
        /// Devolve o tipo de imagem
        /// </summary>
        /// <param name="extension">Extensão da imagem</param>
        /// <returns>ImageFormat</returns>
        public static ImageFormat GetImageFormat(string extension)
        {
            extension = extension.Replace(".", "");

            switch (extension.ToUpper())
            {
                case "JPG":
                case "JPEG":
                    return System.Drawing.Imaging.ImageFormat.Jpeg;
                case "GIF":
                    return System.Drawing.Imaging.ImageFormat.Gif;
                case "BMP":
                    return System.Drawing.Imaging.ImageFormat.Bmp;
                case "PNG":
                default:
                    return System.Drawing.Imaging.ImageFormat.Png;
            }
        }

        public static string GetExtensionFromSimpleBase64(string encoded)
        {
            try
            {
                var img = GetImageFromString(encoded);
                return GetImageFormatExtension(img.RawFormat);
            }
            catch (Exception)
            {
                return "png"; //"idfk";
            }
        }

        public static string GetImageFormatExtension(ImageFormat format)
        {
            try
            {
                return ImageCodecInfo.GetImageEncoders()
                        .First(x => x.FormatID == format.Guid)
                        .FilenameExtension
                        .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .First()
                        .Trim('*').Trim('.')
                        .ToLower();
            }
            catch (Exception)
            {
                return "png"; //"idfk";
            }
        }


        public static System.Drawing.Image GetImageFromString(string base64String)
        {
            byte[] buffer;
            
            buffer = GetImageBufferFromString(base64String);
            
            if (buffer != null)
            {
                ImageConverter ic = new ImageConverter();
                return ic.ConvertFrom(buffer) as System.Drawing.Image;
            }
            else
                return null;
        }

        public static byte[] GetImageBufferFromString(string base64String)
        {
            byte[] buffer;
            try
            {
                buffer = Convert.FromBase64String(base64String);
            }
            catch
            {
                try
                {
                    var str = base64String.Replace(' ', '+');

                    int mod4 = str.Length % 4;
                    if (mod4 > 0)
                    {
                        str += new string('=', 4 - mod4);
                    }
                    buffer = Convert.FromBase64String(str);
                }
                catch
                {
                    var str = HttpUtility.UrlDecode(base64String);
                    buffer = new byte[str.Length * sizeof(char)];
                    System.Buffer.BlockCopy(str.ToCharArray(), 0, buffer, 0, buffer.Length);
                }
            }

            return buffer;
        }

        /// <summary>
        /// Devolve o resource do erro
        /// </summary>
        /// <param name="error">Identificador do erro</param>
        /// <returns>String com a resource a mostrar</returns>
        public static string GetResourceOfError(int error)
        {
            string resourceError = "";
            switch (error)
            {
                //Erro ao obter id Categoria na ListagemConteudos.aspx
                case 1:
                    resourceError = "ERROR_INVALIDCATEGORY";
                    break;
                //Erro ao tentar obter um conteúdo que não é do seu projecto
                case 2:
                    resourceError = "ERROR_CONTEUDOINVALIDO";
                    break;
                //Sessão expirou
                case 3:
                    resourceError = "GLOBAL_SESSAOEXPIROU";
                    break;
                default:
                    resourceError = "ERROR_GENERIC";
                    break;
            }
            return resourceError;
        }

        public static bool GetResourceOfError(string error, out string resourceError)
        {
            int id = 0;
            bool parse = int.TryParse(error, out id);
            if (int.TryParse(error, out id))
                resourceError = GetResourceOfError(id);
            else
                resourceError = "";
            return parse;
        }

        public static string GetImagemEmBase64(string basePath, int idProjecto, string fileName)
        {
            string path = Utils.GetFilePathToSave(basePath, idProjecto, fileName);
            if (File.Exists(path))
                return Convert.ToBase64String(File.ReadAllBytes(path));
            else
                return "";
        }

        public static List<string> GetListaDeExtensoesDeImagensAceites()
        {
            return new List<string> { ".jpg", ".gif", ".jpeg", ".bmp", ".png", ".idfk" };
        }

        public static string ExtractFromString(string text, string start, string end)
        {
            int index_start = text.IndexOf(start), index_end = text.IndexOf(end);

            if (index_start != -1 && index_end != -1)
                return text.Substring(index_start + start.Length, index_end - index_start - start.Length);

            return null;
        }

        public static string FixBase64ForImage(string Image)
        {
            System.Text.StringBuilder sbText = new System.Text.StringBuilder(Image, Image.Length);

            sbText.Replace("\r\n", String.Empty);

            sbText.Replace(" ", String.Empty);

            return sbText.ToString();
        }

        public static Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null) return type;
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(typeName);
                if (type != null)
                    return type;
            }
            return null;
        }
        
        public static object GetPropertyValue( object obj, string name )
        {
            return obj == null ? null : obj.GetType()
                                           .GetProperty( name )
                                           .GetValue( obj, null );
        }

        public static string RemoveDiacritics(string text)
        {
            string newText = text;
            if (newText != null)
                newText = WebUtility.HtmlDecode(newText);

            string formD = newText.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char ch in formD)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
