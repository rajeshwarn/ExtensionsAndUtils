using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Shared.HtmlHelper
{
    /// <summary>
    ///     CheckBox custom HTML helper.
    /// </summary>
    public static class CheckBoxHelper
    {
        #region NullableCheckBoxFor

        /// <summary>
        /// Returns a checkbox for nullable bool (bool?) model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="name">The property name which the value must be set</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        public static MvcHtmlString CheckBoxNullableFor<TModel, TBool>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TBool>> expression, string name, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var value = (metadata.Model as bool?).HasValue && (metadata.Model as bool?).GetValueOrDefault();

            if (String.IsNullOrEmpty(name))
                name = ExpressionHelper.GetExpressionText(expression);

            return htmlHelper.CheckBox(name, value, htmlAttributes);
        }


        /// <summary>
        /// Returns a checkbox for nullable bool (bool?) model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="name">The property name which the value must be set</param>
        public static MvcHtmlString CheckBoxNullableFor<TModel, TBool>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TBool>> expression, string name)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var value = (metadata.Model as bool?).HasValue && (metadata.Model as bool?).GetValueOrDefault();

            if (String.IsNullOrEmpty(name))
                name = ExpressionHelper.GetExpressionText(expression);

            return htmlHelper.CheckBox(name, value);
        }


        /// <summary>
        /// Returns a checkbox for nullable bool (bool?) model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        public static MvcHtmlString CheckBoxNullableFor<TModel, TBool>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TBool>> expression, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var value = (metadata.Model as bool?).HasValue && (metadata.Model as bool?).GetValueOrDefault();

            return htmlHelper.CheckBox(ExpressionHelper.GetExpressionText(expression), value, htmlAttributes);
        }


        /// <summary>
        /// Returns a checkbox for nullable bool (bool?) model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        public static MvcHtmlString CheckBoxNullableFor<TModel, TBool>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TBool>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var value = (metadata.Model as bool?).HasValue && (metadata.Model as bool?).GetValueOrDefault();

            return htmlHelper.CheckBox(ExpressionHelper.GetExpressionText(expression), value);
        }
        #endregion
    }
}