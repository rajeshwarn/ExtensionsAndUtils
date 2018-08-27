using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Shared.Extensions;

namespace Shared.HtmlHelper
{
    /// <summary>
    ///     ValidationErrorMessage custom HTML helper.
    /// </summary>
    public static class HtmlValidation
    {
        /// <summary>
        /// Returns a error message for a complex type model property
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="message">Define a customized message to present</param>
        public static MvcHtmlString ValidationErrorMessageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string message)
        {
            var expressionText = ExpressionHelper.GetExpressionText(expression);

            //cria estrutura para apresentar a mensagem de erro:
            var container = new TagBuilder("span");
            container.MergeAttribute("data-valmsg-for", expressionText);
            container.MergeAttribute("data-valmsg-replace", "true");

            //se a propriedade possuir erros do model, cria o erro a apresentar e adiciona-o à estrutura HTML de cima:
            if (htmlHelper.ViewData.ModelState.CheckIfHasErrorsFor(expressionText))
            {
                container.AddCssClass("field-validation-error");
                var subContainer = new TagBuilder("span");
                subContainer.AddCssClass("");
                subContainer.MergeAttribute("for", expressionText);
                subContainer.InnerHtml += message;
                container.InnerHtml += subContainer.InnerHtml;
            }
            else
            {
                container.AddCssClass("field-validation-valid");
            }
            return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Returns a error message for a complex type model property
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="fieldName">Define the model property to set the error</param>
        /// <param name="message">Define a customized message to present</param>
        public static MvcHtmlString ValidationErrorMessageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string fieldName, string message)
        {
            if (String.IsNullOrEmpty(fieldName))
                fieldName = ExpressionHelper.GetExpressionText(expression);

            //cria estrutura para apresentar a mensagem de erro:
            var container = new TagBuilder("span");
            container.MergeAttribute("data-valmsg-for", fieldName);
            container.MergeAttribute("data-valmsg-replace", "true");

            //se a propriedade possuir erros do model, cria o erro a apresentar e adiciona-o à estrutura HTML de cima:
            if (htmlHelper.ViewData.ModelState.CheckIfHasErrorsFor(fieldName))
            {
                container.AddCssClass("field-validation-error");
                var subContainer = new TagBuilder("span");
                subContainer.AddCssClass("");
                subContainer.MergeAttribute("for", fieldName);
                subContainer.InnerHtml += message;
                container.InnerHtml += subContainer.InnerHtml;
            }
            else
            {
                container.AddCssClass("field-validation-valid");
            }
            return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
        }
    }
}