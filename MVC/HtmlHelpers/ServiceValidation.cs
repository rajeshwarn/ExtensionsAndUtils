using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Shared.HtmlHelper
{
    /// <summary>
    ///     ErrorServiceMessages custom HTML helper.
    /// </summary>
    public static class ErrorMessages
    {
        /// <summary>
        /// Returns a textbox for DateTime model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        public static MvcHtmlString DisplayServiceMsg(this System.Web.Mvc.HtmlHelper htmlHelper)
        {
            //se não houver nem erro nem sucesso não apresenta nada:
            if (htmlHelper.ViewContext.TempData["SvcSUCCESS"] == null && htmlHelper.ViewContext.TempData["SvcERROR"] == null)
                return null;

            //cria estrutura em HTML para apresentar a mensagem:
            var container = new TagBuilder("div");

            //define caracteristicas da estrutura para apresentar mensagem de sucesso:
            if (htmlHelper.ViewContext.TempData["SvcSUCCESS"] != null && !String.IsNullOrEmpty((string)htmlHelper.ViewContext.TempData["SvcSUCCESS"]))
            {
                container.MergeAttribute("id", "isSuccessValidationDiv");
                container.AddCssClass("SvcMsg");
                container.AddCssClass("successValidationMsg");
                container.InnerHtml += htmlHelper.ViewContext.TempData["SvcSUCCESS"].ToString();
                htmlHelper.ViewContext.TempData.Remove("SvcSUCCESS");

                return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
            }

            //define caracteristicas da estrutura para apresentar mensagem de erro:
            if (htmlHelper.ViewContext.TempData["SvcERROR"] != null && !String.IsNullOrEmpty((string)htmlHelper.ViewContext.TempData["SvcERROR"]))
            {
                container.MergeAttribute("id", "isErrorValidationDiv");
                container.AddCssClass("SvcMsg");
                container.AddCssClass("errorValidationMsg");
                container.InnerHtml += htmlHelper.ViewContext.TempData["SvcERROR"].ToString();
                htmlHelper.ViewContext.TempData.Remove("SvcERROR");

                return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
            }

            //para a mensagem ficar com o aspeto desejado deve-se possuir um ficheiro CSS adequado (service-msg.css)
            //para a mensagem apresentar a interatividade pretendida (desaparecer sozinha ou ao clicar) deve-se executar na classe app.js a função appUtils.displaySvcMsg();

            return null;
        }


        /// <summary>
        /// Returns a textbox for DateTime model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        public static MvcHtmlString DisplayServiceMsg(this System.Web.Mvc.HtmlHelper htmlHelper, object htmlAttributes)
        {
            //se não houver nem erro nem sucesso não apresenta nada:
            if (htmlHelper.ViewContext.TempData["SvcSUCCESS"] == null && htmlHelper.ViewContext.TempData["SvcERROR"] == null)
                return null;

            //cria estrutura em HTML para apresentar a mensagem:
            var container = new TagBuilder("div");
            if (htmlAttributes != null)
                container.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            //define caracteristicas da estrutura para apresentar mensagem de sucesso:
            if (htmlHelper.ViewContext.TempData["SvcSUCCESS"] != null && !String.IsNullOrEmpty((string)htmlHelper.ViewContext.TempData["SvcSUCCESS"]))
            {
                container.MergeAttribute("id", "isSuccessValidationDiv");
                container.AddCssClass("SvcMsg");
                container.AddCssClass("successValidationMsg");
                container.InnerHtml += htmlHelper.ViewContext.TempData["SvcSUCCESS"].ToString();
                htmlHelper.ViewContext.TempData.Remove("SvcSUCCESS");

                return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
            }

            //define caracteristicas da estrutura para apresentar mensagem de erro:
            if (htmlHelper.ViewContext.TempData["SvcERROR"] != null && !String.IsNullOrEmpty((string)htmlHelper.ViewContext.TempData["SvcERROR"]))
            {
                container.MergeAttribute("id", "isErrorValidationDiv");
                container.AddCssClass("SvcMsg");
                container.AddCssClass("errorValidationMsg");
                container.InnerHtml += htmlHelper.ViewContext.TempData["SvcERROR"].ToString();
                htmlHelper.ViewContext.TempData.Remove("SvcERROR");

                return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
            }

            //para a mensagem ficar com o aspeto desejado deve-se possuir um ficheiro CSS adequado (service-msg.css)
            //para a mensagem apresentar a interatividade pretendida (desaparecer sozinha ou ao clicar) deve-se executar na classe app.js a função appUtils.displaySvcMsg();

            return null;
        }
    }
}