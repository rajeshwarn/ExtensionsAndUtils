using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Shared.Extensions;
using Model.WebcallResponse;

namespace Model.Validations
{
    public static class ServiceValidation
    {
        public static void CleanServiceMsg(ControllerContext controller)
        {
            CleanMsg(controller);
        }

        private static void CleanMsg(ControllerContext controller)
        {
            controller.Controller.TempData.Remove("SvcERROR");
            controller.Controller.TempData.Remove("SvcSUCCESS");
        }

        public static void CheckServiceMsg(ControllerContext controller, Status responseStatus, ICollection<string> responseMsg = null, string successMsg = null)
        {
            if (responseStatus == Status.SUCCESS)
            {
                if (!String.IsNullOrEmpty(successMsg))
                    controller.Controller.TempData["SvcSUCCESS"] = successMsg;
                else
                    controller.Controller.TempData["SvcSUCCESS"] = "Operação concluída com sucesso";
            }
            else
            {
                if (responseMsg.SafeAny())
                {
                    var message = new StringBuilder();

                    foreach (var msg in responseMsg.Where(msg => !String.IsNullOrEmpty(msg)))
                        message.AppendLine(msg + "</br>");

                    if (!String.IsNullOrEmpty(message.ToString()))
                        controller.Controller.TempData["SvcERROR"] = message.ToString();
                }
                else
                {
                    controller.Controller.TempData["SvcERROR"] = "Não foi possivel concluir a operação";
                }
            }
        }

        public static void CheckServiceMsg(ControllerContext controller, Status responseStatus, bool boolResult, ICollection<string> responseMsg = null, string successMsg = null)
        {
            if((boolResult == false && responseStatus == Status.SUCCESS) || (boolResult == true && responseStatus == Status.FAIL))
                responseStatus=Status.FAIL;

            if (responseStatus == Status.SUCCESS)
            {
                if (!String.IsNullOrEmpty(successMsg))
                    controller.Controller.TempData["SvcSUCCESS"] = successMsg;
                else
                    controller.Controller.TempData["SvcSUCCESS"] = "Operação concluída com sucesso";
            }
            else
            {
                if (responseMsg.SafeAny())
                {
                    var message = new StringBuilder();

                    foreach (var msg in responseMsg.Where(msg => !String.IsNullOrEmpty(msg)))
                        message.AppendLine(msg + "</br>");

                    if (!String.IsNullOrEmpty(message.ToString()))
                        controller.Controller.TempData["SvcERROR"] = message.ToString();
                }
                else
                {
                    controller.Controller.TempData["SvcERROR"] = "Não foi possivel concluir a operação";
                }
            }
        }
        
        public static void CheckServiceMsg(ControllerContext controller, string exceptionMsg)
        {
                controller.Controller.TempData["SvcERROR"] = "ERRO: " + exceptionMsg;
                controller.Controller.TempData.Remove("SvcSUCCESS");
        }
    }
}