using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace BLL.ProxyAPI
{
    public class ProxyAPI //: IDisposable
    {
        private static WebServiceAPI ClientService = new WebServiceAPI("http://.../example.asmx");    // DEFAULT location of the WebService, containing the WebMethods

        public ProxyAPI()
        { }

        public ProxyAPI(string webserviceEndpoint)
        { ClientService = new WebServiceAPI(webserviceEndpoint); }

        public static void ChangeUrl(string webserviceEndpoint)
        {
            ClientService = new WebServiceAPI(webserviceEndpoint);
        }

        public static string CallWebMethod(string name, int value)
        {
            ClientService.PreInvoke();

            ClientService.AddParameter("name", name);                    // Case Sensitive! To avoid typos, just copy the WebMethod's signature and paste it
            ClientService.AddParameter("value", value.ToString());     // all parameters are passed as strings
            try
            {
                ClientService.Invoke("CallWebMethod");                // name of the WebMethod to call (Case Sentitive again!)
            }
            finally { ClientService.PosInvoke(); }

            return ClientService.ResultString;                           // you can either return a string or an XML, your choice
        }

        public string CallWebMethod(string method, Dictionary<string, string> parameters = null)
        {
            ClientService.PreInvoke();

            if (parameters != null && parameters.Any())
            {
                // Case Sensitive! To avoid typos, just copy the WebMethod's signature and paste it // all parameters are passed as strings  
                parameters.ToList().ForEach(dic => ClientService.AddParameter(dic.Key, dic.Value));
            }

            try
            {
                ClientService.Invoke(method);                // name of the WebMethod to call (Case Sentitive again!)
            }
            finally { ClientService.PosInvoke(); }

            return ClientService.ResultString;               // you can either return a string or an XML, your choice
        }

        public XDocument CallSoapWebMethod(string method, Dictionary<string, string> parameters = null)
        {
            ClientService.PreInvoke();

            if (parameters != null && parameters.Any())
            {
                // Case Sensitive! To avoid typos, just copy the WebMethod's signature and paste it // all parameters are passed as strings  
                parameters.ToList().ForEach(dic => ClientService.AddParameter(dic.Key, dic.Value));
            }

            try
            {
                ClientService.Invoke(method);                // name of the WebMethod to call (Case Sentitive again!)
            }
            finally { ClientService.PosInvoke(); }

            return ClientService.ResponseSOAP;       // you can either return a string or an XML, your choice
        }


        public XDocument CallXmlWebMethod(string method, Dictionary<string, string> parameters = null)
        {
            ClientService.PreInvoke();

            if (parameters != null && parameters.Any())
            {
                // Case Sensitive! To avoid typos, just copy the WebMethod's signature and paste it // all parameters are passed as strings  
                parameters.ToList().ForEach(dic => ClientService.AddParameter(dic.Key, dic.Value));
            }

            try
            {
                ClientService.Invoke(method);                // name of the WebMethod to call (Case Sentitive again!)
            }
            finally { ClientService.PosInvoke(); }

            return ClientService.ResultXML;               // you can either return a string or an XML, your choice
        }

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
