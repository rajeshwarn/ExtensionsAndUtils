using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Shared.HtmlHelper.Resources;

namespace Shared.HtmlHelper
{
    /// <summary>
    ///     Warning custom HTML helper.
    /// </summary>
    public static class WarningHelper
    {
        /// <summary>
        /// Returns a structure with a warning message
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="warningText">Defines the warning text to present</param>
        /// <param name="imgSource">Defines the warning image path to present</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        public static MvcHtmlString Warning(this System.Web.Mvc.HtmlHelper htmlHelper, string warningText, string imgSource = null, object htmlAttributes = null)
        {
            if (String.IsNullOrEmpty(warningText)) return null;

            //cria estrutura HTML base para conter o aviso:
            var container = new TagBuilder("div");

            if (htmlAttributes != null)
                container.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            //atribui estilo base de apresentação do aviso:
            container.AddCssClass("label-warning");

            //cria imagem a apresentar no aviso:
            var imgContainer = new TagBuilder("span");
            var img = new TagBuilder("img");
            img.MergeAttribute("alt","");

            if(!String.IsNullOrEmpty(imgSource))
                img.MergeAttribute("src", imgSource);

            imgContainer.InnerHtml = img.ToString();

            //constrói aviso:
            var warningContainer = new TagBuilder("span");
            warningContainer.InnerHtml = "<strong>" + HelpersResx.Word_Warning + " </strong>" + warningText;

            //adiciona a imagem e o aviso à estrutura HTML base:
            container.InnerHtml += imgContainer;
            container.InnerHtml += warningContainer;

            return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Returns a structure with a warning message
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="warningText">Defines the warning text to present</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        public static MvcHtmlString Warning(this System.Web.Mvc.HtmlHelper htmlHelper, string warningText, object htmlAttributes)
        {
            return Warning(htmlHelper, warningText, null, htmlAttributes);
        }


        /// <summary>
        /// Returns a structure with a warning message
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="warnings">Defines the warnings text list to present</param>
        /// <param name="imgSource">Defines the warning image path to present</param>
        public static MvcHtmlString WarningsList(this System.Web.Mvc.HtmlHelper htmlHelper, List<string> warnings, string imgSource = null)
        {
            if (warnings == null || !warnings.Any()) return null;

            //cria estrutura HTML base para conter o aviso:
            var table = new TagBuilder("table");
            table.AddCssClass("label-warning");
            var header = new TagBuilder("tr");
            var headerTd = new TagBuilder("td");

            var warningsLine = new TagBuilder("tr");
            var warningsTd = new TagBuilder("td");

            var headerContainer = new TagBuilder("div");
            headerContainer.AddCssClass("general-features");

            //cria imagem a apresentar no aviso:
            var imgContainer = new TagBuilder("span");
            var img = new TagBuilder("img");
            img.MergeAttribute("alt", "");

            if (!String.IsNullOrEmpty(imgSource))
                img.MergeAttribute("src", imgSource);

            imgContainer.InnerHtml = img.ToString();

            //cria introdução do aviso:
            var warningTitle = new TagBuilder("span") { InnerHtml = "<strong>" + HelpersResx.Word_Warning + " </strong>" };


            var warningsContainer = new TagBuilder("div");
            warningsContainer.AddCssClass("warnings-features");

            //constrói lista de avisos:
            var warningsList = new TagBuilder("ul");

            foreach (var warning in warnings.Where(warning => !String.IsNullOrEmpty(warning) && !String.IsNullOrWhiteSpace(warning)))
            {
                var warningsSpan = new TagBuilder("li");
                warningsSpan.InnerHtml += warning;
                if (!String.IsNullOrEmpty(warningsSpan.InnerHtml))
                warningsList.InnerHtml += warningsSpan;
            }

            if (String.IsNullOrEmpty(warningsList.InnerHtml)) return null;

            //adiciona tudo à estrutura HTML base:
            headerContainer.InnerHtml += imgContainer;
            headerContainer.InnerHtml += warningTitle;
            headerTd.InnerHtml += headerContainer;
            header.InnerHtml += headerTd;
            table.InnerHtml += header;

            warningsContainer.InnerHtml += warningsList;
            warningsTd.InnerHtml += warningsContainer;
            warningsLine.InnerHtml += warningsTd;
            table.InnerHtml += warningsLine;

            return MvcHtmlString.Create(table.ToString(TagRenderMode.Normal));
        }
    }
}