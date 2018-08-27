using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Web.Routing;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Web.Mvc.Html;
using PDS.eVacinas.Extensions.ModelState;
using PDS.eVacinas.Model.PNV;
using PDS.eVacinas.Model.Vaccines;
using PDS.eVacinas.Model.WebcallResponse;
using PDS.Mockup.PI.Resources;
using WebGrease.Css.Extensions;

namespace Helpers
{
    public static class ValidationMsgHelperExtension
    {

        public static MvcHtmlString DisplayServiceMsg(this HtmlHelper html)
        {
            if (html.ViewContext.TempData["SvcSUCCESS"] == null && html.ViewContext.TempData["SvcERROR"] == null)
                return null;

            TagBuilder container = new TagBuilder("div");

            if (html.ViewContext.TempData["SvcSUCCESS"] != null)
            {
                container.MergeAttribute("id", "isSuccessValidationDiv");
                container.AddCssClass("SvcMsg");
                container.AddCssClass("successValidationMsg");
                container.InnerHtml += html.ViewContext.TempData["SvcSUCCESS"].ToString();
                html.ViewContext.TempData.Remove("SvcSUCCESS");

                //TagBuilder button = new TagBuilder("a");
                //button.AddCssClass("jqButton");
                //button.MergeAttribute("style", "margin-left: 20px");
                //button.InnerHtml += "Ok";

                //container.InnerHtml += "<br/><br/>";
                //container.InnerHtml += button;

                return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
            }

            if (html.ViewContext.TempData["SvcERROR"] != null)
            {
                container.MergeAttribute("id", "isErrorValidationDiv");
                container.AddCssClass("SvcMsg");
                container.AddCssClass("errorValidationMsg");
                container.InnerHtml += html.ViewContext.TempData["SvcERROR"].ToString();
                html.ViewContext.TempData.Remove("SvcERROR");

                //TagBuilder button = new TagBuilder("a");
                //button.AddCssClass("jqButton");
                //button.MergeAttribute("style", "margin-left: 20px");
                //button.InnerHtml += "Ok";

                //container.InnerHtml += "<br/><br/>";
                //container.InnerHtml += button;

                return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
            }

            return null;
        }

        public static MvcHtmlString DisplayServiceMsg(this HtmlHelper html, object htmlAttributes)
        {
            if (html.ViewContext.TempData["SvcSUCCESS"] == null && html.ViewContext.TempData["SvcERROR"] == null)
                return null;

            TagBuilder container = new TagBuilder("div");
            if (htmlAttributes != null)
                container.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            if (html.ViewContext.TempData["SvcSUCCESS"] != null)
            {
                container.MergeAttribute("id", "isSuccessValidationDiv");
                container.AddCssClass("SvcMsg");
                container.AddCssClass("successValidationMsg");
                container.InnerHtml += html.ViewContext.TempData["SvcSUCCESS"].ToString();
                html.ViewContext.TempData.Remove("SvcSUCCESS");

                //TagBuilder button = new TagBuilder("a");
                //button.AddCssClass("jqButton");
                //button.MergeAttribute("style", "margin-left: 20px");
                //button.InnerHtml += "Ok";

                //container.InnerHtml += "<br/><br/>";
                //container.InnerHtml += button;

                return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
            }

            if (html.ViewContext.TempData["SvcERROR"] != null)
            {
                container.MergeAttribute("id", "isErrorValidationDiv");
                container.AddCssClass("SvcMsg");
                container.AddCssClass("errorValidationMsg");
                container.InnerHtml += html.ViewContext.TempData["SvcERROR"].ToString();
                html.ViewContext.TempData.Remove("SvcERROR");

                //TagBuilder button = new TagBuilder("a");
                //button.AddCssClass("jqButton");
                //button.MergeAttribute("style", "margin-left: 20px");
                //button.InnerHtml += "Ok";

                //container.InnerHtml += "<br/><br/>";
                //container.InnerHtml += button;

                return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
            }

            return null;
        }

        public static void CheckServiceMsg(ControllerContext controller, Status responseStatus, ICollection<string> responseMsg, string successMsg)
        {
            if (responseStatus == Status.SUCCESS)
            {
                controller.Controller.TempData["SvcSUCCESS"] = successMsg;
                controller.Controller.TempData.Remove("SvcERROR");
            }
            else
            {
                StringBuilder message = new StringBuilder();
                foreach (string msg in responseMsg)
                    message.AppendLine(msg);

                controller.Controller.TempData["SvcERROR"] = message;
                controller.Controller.TempData.Remove("SvcSUCCESS");
            }
        }


        public static MvcHtmlString DisplaySucessMsg(this HtmlHelper html, string successKey)
        {
            if (String.IsNullOrEmpty(successKey) || html.ViewContext.TempData[successKey] == null)
                return null;

            TagBuilder container = new TagBuilder("div");
            container.MergeAttribute("id", "isSuccessValidationDiv");
            container.AddCssClass("successValidationMsg");
            container.MergeAttribute("style", "display: none"); //pode ser feito através da class
            container.InnerHtml += html.ViewContext.TempData[successKey].ToString();

            return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString DisplayExceptionMsg(this HtmlHelper html, string errorKey)
        {
            if (String.IsNullOrEmpty(errorKey) || html.ViewContext.TempData[errorKey] == null)
                return null;

            TagBuilder container = new TagBuilder("div");
            container.MergeAttribute("id", "isErrorValidationDiv");
            container.AddCssClass("errorValidationMsg");
            container.MergeAttribute("style", "display: none"); //pode ser feito através da class
            container.InnerHtml += html.ViewContext.TempData[errorKey].ToString();

            return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString DisplayErrorMsgFor<TModel, TProp>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProp>> expression, string errorKey)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            if (String.IsNullOrEmpty(errorKey) || html.ViewContext.TempData[errorKey] == null || String.IsNullOrEmpty(metadata.Model.ToString()))
                return null;

            if (html.ViewData.ModelState.CheckIfHasErrorsFor<TModel>(expression as Expression<Func<TModel, object>>))
            {
                TagBuilder container = new TagBuilder("div");
                container.MergeAttribute("id", "isErrorValidationDiv");
                container.AddCssClass("errorValidationMsg");
                container.MergeAttribute("style", "display: none"); //pode ser feito através da class
                container.InnerHtml += html.ViewContext.TempData[errorKey].ToString();

                return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
            }
            return null;
        }


        #region ValidationErrorMessageFor tables
        public static MvcHtmlString ValidationErrorMessageFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string message)
        {
            string expressionText = ExpressionHelper.GetExpressionText(expression);

            if (html.ViewData.ModelState.CheckIfHasErrorsFor<TModel>(expressionText)) //html.ViewData.ModelState.IsValid
            {
                TagBuilder container = new TagBuilder("div");
                container.AddCssClass("text-valSummary");
                container.MergeAttribute("id", expressionText);

                TagBuilder subContainer = new TagBuilder("div");
                subContainer.AddCssClass("text-errorSummary");
                subContainer.MergeAttribute("style", "text-decoration: none");
                subContainer.InnerHtml += message;

                container.InnerHtml += subContainer;
                
                return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
            }
            return null;
        }

        public static MvcHtmlString ValidationErrorMessageFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string fieldName, string message)
        {
            if (String.IsNullOrEmpty(fieldName))
                fieldName = ExpressionHelper.GetExpressionText(expression);

            TagBuilder container = new TagBuilder("span");
            container.AddCssClass("field-validation-valid");
            container.MergeAttribute("data-valmsg-for", fieldName);
            container.MergeAttribute("data-valmsg-replace", "true");

            if (html.ViewData.ModelState.CheckIfHasErrorsFor<TModel>(fieldName))
            {
                TagBuilder subContainer = new TagBuilder("span");
                subContainer.AddCssClass("");
                subContainer.MergeAttribute("for", fieldName);
                subContainer.InnerHtml += message;
                container.InnerHtml += subContainer.InnerHtml;
            }
            return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
        }
        #endregion

    }
}