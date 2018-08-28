using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Serialize
/// </summary>
/// 

namespace BLL.Communications.Serialization
{
    public class Serialize
    {
        public static string SerializeAndCompress<T>(T obj)
        {
            string compressedSerializedXML = SerializeXml.SerializeObject(obj, typeof(T));
            //return CompressGZip.Compress(compressedSerializedXML);
            return CompressGZip.deflate(compressedSerializedXML);
        }

        public static Object DeserializeAndDecompress<T>(string obj)
        {
            //string decompressedObj = CompressGZip.Decompress(obj);
            string decompressedObj = CompressGZip.inflate(obj);
            return SerializeXml.DeserializeObject(decompressedObj, typeof(T));
        }
    }
}