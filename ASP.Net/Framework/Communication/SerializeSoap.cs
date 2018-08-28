using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Linq;
using System.Xml;
using System.ServiceModel;

using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Xml.Serialization;

namespace Framework
{
    public static class SerializeSoap
    {
        /// <summary>
        /// Converts a SOAP string to an object
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="SOAP">SOAP string</param>
        /// <returns>The object of the specified type</returns>
        public static T SOAPToObject<T>(string SOAP)
        {
            if (string.IsNullOrEmpty(SOAP))
            {
                throw new ArgumentException("SOAP can not be null/empty");
            }
            using (MemoryStream Stream = new MemoryStream(UTF8Encoding.UTF8.GetBytes(SOAP)))
            {
                SoapFormatter Formatter = new SoapFormatter();
                return (T)Formatter.Deserialize(Stream);
            }
        }

        public static T SOAPToObject<T>(XDocument SOAP)
        {
            if (SOAP == null)
            {
                throw new ArgumentException("SOAP can not be null/empty");
            }

            using (var reader = XmlReader.Create(new StringReader(SOAP.ToString())))
            {
                Message msg = Message.CreateMessage(reader, int.MaxValue, MessageVersion.Soap11);
                return msg.GetBody<T>();
            }
        }

        public static T SOAPXmlToObject<T>(XDocument SOAP)
        {
            if (SOAP == null)
            {
                throw new ArgumentException("SOAP can not be null/empty");
            }

            Message message = Message.CreateMessage(XmlReader.Create(new StringReader(SOAP.ToString())), int.MaxValue, MessageVersion.Soap11);
            
            MemoryStream ms = new MemoryStream();
            XmlWriter w = XmlWriter.Create(ms, new XmlWriterSettings { Indent = true, IndentChars = "  ", OmitXmlDeclaration = true });
            XmlDictionaryReader bodyReader = message.GetReaderAtBodyContents();
            
            w.WriteStartElement("s", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
            
            while (bodyReader.NodeType != XmlNodeType.EndElement && bodyReader.LocalName != "Body" && bodyReader.NamespaceURI != "http://schemas.xmlsoap.org/soap/envelope/")
            {
                if (bodyReader.NodeType != XmlNodeType.Whitespace)
                {
                    w.WriteNode(bodyReader, true);
                }
                else
                {
                    bodyReader.Read(); // ignore whitespace; maintain if you want
                }
            }

            w.WriteEndElement();
            w.Flush();

            ms.Position = 0;
            XmlDocument doc = new XmlDocument();
            doc.Load(ms);

            var retifiedMsg = new XDocument(RetifyXmlBody(XElement.Parse(doc.DocumentElement.InnerXml)));

            return (T)SerializeXml.DeserializeObject(retifiedMsg.ToString(), typeof(T));
        }

        private static XElement RetifyXmlBody(XElement xmlContainer)
        {
            if (!xmlContainer.HasElements)
            {
                XElement xElement = new XElement(xmlContainer.Name.LocalName);
                xElement.Value = xmlContainer.Value;

                foreach (XAttribute attribute in xmlContainer.Attributes())
                    xElement.Add(attribute);

                return xElement;
            }
            return new XElement(xmlContainer.Name.LocalName, xmlContainer.Elements().Select(el => RetifyXmlBody(el)));
        }

         
        /// <summary>
        /// Converts an object to a SOAP string
        /// </summary>
        /// <param name="Object">Object to serialize</param>
        /// <returns>The serialized string</returns>
        public static string ObjectToSOAP(object Object)
        {
            if (Object == null)
            {
                throw new ArgumentException("Object can not be null");
            }
            using (MemoryStream Stream = new MemoryStream())
            {
                SoapFormatter Serializer = new SoapFormatter();
                Serializer.Serialize(Stream, Object);
                Stream.Flush();
                return UTF8Encoding.UTF8.GetString(Stream.GetBuffer(), 0, (int)Stream.Position);
            }
        }
    }
}
