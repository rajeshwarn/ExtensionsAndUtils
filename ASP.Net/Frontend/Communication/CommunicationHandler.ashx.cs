using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using Framework;

namespace Communication
{
    /// <summary>
    /// Summary description for CommunicationHandler
    /// </summary>
    public class CommunicationHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var method = ""; var parameters = ""; var result = ""; 


                /*Obtem dados do pedido*/                
                if(context.Request.RequestType == "GET" && context.Request.QueryString.AllKeys.Any(m=>m == "request"))
                {
                    //pedido GET
                    method = context.Request.QueryString.Get("method");
                    parameters = context.Request.QueryString.Get("parameters");

                }                
                else if (context.Request.RequestType == "POST")
                {
                    //pedido POST
                    using (var reader = new StreamReader(context.Request.InputStream))
                        parameters = reader.ReadToEnd();

                    method = context.Request.PathInfo.Replace(@"/","");
                }


                /*Recolhe e transforma os dados*/
                var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DateFormatHandling = DateFormatHandling.IsoDateFormat };
                var json = (JObject)JsonConvert.DeserializeObject(parameters, settings);

                var methodCall = typeof(AJAXCommunication).GetMethod(method);
                var parametersCall = GetObjectChildren(json, methodCall.GetParameters(), settings);

                
                /*Invoca o método já com os parametros*/
                object resultCall = methodCall.Invoke(new AJAXCommunication(), parametersCall);


                /*Cria resposta para o AJAX/JS*/
                var objResponse = new JObject(new JProperty(method + "Result", JObject.FromObject(resultCall)));
                result = JsonConvert.SerializeObject(objResponse);


                context.Response.ContentType = "application/json; charset=utf-8";
                context.Response.Write(result);
            }
            catch {

                context.Response.ContentType = "application/json; charset=utf-8";
                context.Response.Write("Lamentamos mas não foi possivel efetuar a operação pretendida");
            }
        }

        private object[] GetObjectChildren(JObject json, ParameterInfo[] parameters, JsonSerializerSettings settings)
        {
            var serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue, RecursionLimit = 10000 };

            var children = new List<object>();
            parameters.ToList().ForEach(param => {
                if (json[param.Name] != null)
                {
                    if (json[param.Name].Children().Any())
                        children.Add(serializer.Deserialize(json[param.Name].ToString(), param.ParameterType));
                    else
                        children.Add(Convert.ChangeType(json[param.Name], param.ParameterType));
                }
                else
                {
                    if (param.ParameterType == typeof(double) || param.ParameterType == typeof(decimal) || param.ParameterType == typeof(int))
                        children.Add(Convert.ChangeType(0, param.ParameterType));
                    else if (param.ParameterType == typeof(bool))
                        children.Add(Convert.ChangeType(false, param.ParameterType));
                    else
                        children.Add(Convert.ChangeType(null, param.ParameterType));
                }
            });

            return children.ToArray();
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