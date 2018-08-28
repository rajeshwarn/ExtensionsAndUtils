using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for CompressGZip
/// </summary>
/// 
namespace BLL.Communications.Serialization
{
    public class CompressGZip
    {
        #region Deflate compression methods

        //Deflate compress mode
        public static string deflate(string text)
        {

            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] retVal;

            using (MemoryStream compressedMemoryStream = new MemoryStream())
            {
                DeflateStream compressStream = new DeflateStream(compressedMemoryStream, CompressionMode.Compress, true);
                compressStream.Write(data, 0, data.Length);
                compressStream.Close();
                retVal = new byte[compressedMemoryStream.Length];
                compressedMemoryStream.Position = 0L;
                compressedMemoryStream.Read(retVal, 0, retVal.Length);
                compressedMemoryStream.Close();
                compressStream.Close();
            }

            return Convert.ToBase64String(retVal);

        }

        //Deflate decompress mode
        public static string inflate(string compressedText)
        {
            byte[] data = Convert.FromBase64String(compressedText);

            byte[] retVal;
            using (MemoryStream compressedMemoryStream = new MemoryStream())
            {
                compressedMemoryStream.Write(data, 0, data.Length);
                compressedMemoryStream.Position = 0L;
                MemoryStream decompressedMemoryStream = new MemoryStream();
                DeflateStream decompressStream = new DeflateStream(compressedMemoryStream, CompressionMode.Decompress);
                decompressStream.CopyTo(decompressedMemoryStream);
                retVal = new byte[decompressedMemoryStream.Length];
                decompressedMemoryStream.Position = 0L;
                decompressedMemoryStream.Read(retVal, 0, retVal.Length);
                compressedMemoryStream.Close();
                decompressedMemoryStream.Close();
                decompressStream.Close();
            }

            return Encoding.UTF8.GetString(retVal);

        }

        #endregion

        public static string Compress(string text)
        {
            //Encoding.ASCII.GetBytes(text);
            //Encoding.UTF8.GetBytes(text);

            byte[] buffer = Encoding.UTF8.GetBytes(text);
            MemoryStream ms = new MemoryStream();
            using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
            {
                zip.Write(buffer, 0, buffer.Length);
            }

            ms.Position = 0;
            MemoryStream outStream = new MemoryStream();

            byte[] compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);

            byte[] gzBuffer = new byte[compressed.Length + 4];
            System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);

            return Convert.ToBase64String(gzBuffer);
            //return gzBuffer;
        }

        public static string Decompress(string compressedText)
        {
            byte[] gzBuffer = Convert.FromBase64String(compressedText);
            using (MemoryStream ms = new MemoryStream())
            {
                int msgLength = BitConverter.ToInt32(gzBuffer, 0);
                int size = gzBuffer.Length - 4;
                ms.Write(gzBuffer, 4, size);

                byte[] buffer = new byte[msgLength];

                ms.Position = 0;
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    zip.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }
    }
}