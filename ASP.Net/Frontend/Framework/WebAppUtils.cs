using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using Framework;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Framework.Enumerators;

namespace Framework
{
    public class WebAppUtils
    {
        /// <summary>
        /// Obtem lista de extensões de imagens aceites pelo BackOffice
        /// </summary>
        /// <returns>Lista de strings com as extensões aceites</returns>
        public static List<string> GetAllowedImgExtensions()
        {
            return new List<string> { ".jpg", ".gif", ".jpeg", ".bmp", ".png" };
        }


        /// <summary>
        /// Guarda uma imagem com compressão no disco
        /// </summary>
        /// <param name="image">Imagem a guardar</param>
        /// <param name="filename">Nome da imagem</param>
        /// <param name="fullpath">Caminho completo para onde guardar (com nome incluido)</param>
        /// <param name="size">Tamanho da imagem</param>
        public static void SaveImage(System.Drawing.Image image, string filename, string fullpath, int size)
        {
            try
            {
                int ratio = int.Parse(ConfigurationManager.AppSettings["Compression_Small"].ToString());
                //se a imagem for maior do que 512KB e menor do que 1MB
                if (size < 1048576)
                {
                    using (var encoderParameters = new EncoderParameters(1))
                    {
                        var imageCodecInfo = ImageCodecInfo.GetImageEncoders().First(encoder => String.Compare(encoder.MimeType, Utils.GetMimeType(filename), StringComparison.OrdinalIgnoreCase) == 0);

                        encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, Convert.ToInt64(75));
                        image.Save(fullpath, imageCodecInfo, encoderParameters);
                    }
                }
                //se a imagem for maior do que 1MB e menor do que 2MB
                else if (size < 2097152)
                {
                    ratio = int.Parse(ConfigurationManager.AppSettings["Compression_Medium"].ToString());
                    using (var encoderParameters = new EncoderParameters(1))
                    {
                        var imageCodecInfo = ImageCodecInfo.GetImageEncoders().First(encoder => String.Compare(encoder.MimeType, Utils.GetMimeType(filename), StringComparison.OrdinalIgnoreCase) == 0);

                        encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, Convert.ToInt64(65));
                        image.Save(fullpath, imageCodecInfo, encoderParameters);
                    }
                }
                //se a imagem for maior do que 2MB
                else
                {
                    ratio = int.Parse(ConfigurationManager.AppSettings["Compression_Large"].ToString());
                    using (var encoderParameters = new EncoderParameters(1))
                    {
                        var imageCodecInfo = ImageCodecInfo.GetImageEncoders().First(encoder => String.Compare(encoder.MimeType, Utils.GetMimeType(filename), StringComparison.OrdinalIgnoreCase) == 0);

                        encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, Convert.ToInt64(50));
                        image.Save(fullpath, imageCodecInfo, encoderParameters);
                    }
                }
            }
            catch { throw; }
        }



        public static GlobalInfo DecryptGlobalInfo(string globalInfo)
        {
            var serialize = new JavaScriptSerializer();

            GlobalInfo newObjectGlobal = serialize.Deserialize<GlobalInfo>(Encryption.Decrypt(globalInfo));

            return newObjectGlobal;
        }



        public static string GetFilesPath(int idProject, string module)
        {
            return String.Format(ConfigurationManager.AppSettings["FilesPath"].ToString(), idProject) + module + "\\";
        }
    }
}