using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
/// <summary>
/// Summary description for SerializeXml
/// </summary>
namespace BLL.Communications.Serialization
{

    public class SerializeXml
    {

        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        private static String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        /// <summary>
        /// Converts the String to UTF8 Byte array and is used in De serialization
        /// </summary>
        /// <param name="pXmlString"></param>
        /// <returns></returns>
        private static Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        /// <summary>
        /// Method to reconstruct an Object from XML string
        /// </summary>
        /// <param name="pXmlizedString"></param>
        /// <returns></returns>
        public static Object DeserializeObject(String pXmlizedString, Type typeOf, bool decodeHTML = false)
        {
            XmlSerializer xs = new XmlSerializer(typeOf);
            MemoryStream memoryStream;

            if (decodeHTML)
                memoryStream = new MemoryStream(StringToUTF8ByteArray(HttpUtility.HtmlDecode(pXmlizedString)));
            else
                memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));

            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

            return xs.Deserialize(memoryStream);
        }

        public static String SerializeObject<T>(T pObject)
        {
            return SerializeObject(pObject, typeof(T));
        }
        /// <summary>
        /// Method to convert a custom Object to XML string
        /// </summary>
        /// <param name="pObject">Object that is to be serialized to XML</param>
        /// <returns>XML string</returns>
        public static String SerializeObject(Object pObject, Type typeOf)
        {
            try
            {
                String XmlizedString = null;
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer xs = new XmlSerializer(typeOf);
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

                xs.Serialize(xmlTextWriter, pObject);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
                XmlizedString = XmlizedString.Remove(0, 1);
                return XmlizedString.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "").Replace("xsi:nil=\"true\"", "");
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                return null;
            }
        }
    }
}