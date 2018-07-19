using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Shared.HtmlHelper
{
    /// <summary>
    ///     DateTime custom HTML helper.
    /// </summary>
    public static class DateTimeHelper
    {
        /*
        #region DateTimeFor
        
        /// <summary>
        /// Returns a textbox for DateTime model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="name">The property name which the value must be set</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        public static MvcHtmlString DateTimeFor<TModel, TDateTime>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TDateTime>> expression, string name, object htmlAttributes)
        {
            var date = Convert.ToDateTime(ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model);
            object value = date.ToShortDateString();

            if (String.IsNullOrEmpty(name))
                name = ExpressionHelper.GetExpressionText(expression);
                
            return htmlHelper.TextBox(name, value, htmlAttributes);
        }

        /// <summary>
        /// Returns a textbox for DateTime model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="name">The property name which the value must be set</param>
        public static MvcHtmlString DateTimeFor<TModel, TDateTime>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TDateTime>> expression, string name)
        {
            var date = Convert.ToDateTime(ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model);
            object value = date.ToShortDateString();

            if (String.IsNullOrEmpty(name))
                name = ExpressionHelper.GetExpressionText(expression);

            return htmlHelper.TextBox(name, value);
        }

        /// <summary>
        /// Returns a textbox for DateTime model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        public static MvcHtmlString DateTimeFor<TModel, TDateTime>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TDateTime>> expression, object htmlAttributes)
        {
            var date = Convert.ToDateTime(ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model);
            object value = date.ToShortDateString();

            return htmlHelper.TextBox(ExpressionHelper.GetExpressionText(expression), value, htmlAttributes);
        }

        /// <summary>
        /// Returns a textbox for DateTime model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        public static MvcHtmlString DateTimeFor<TModel, TDateTime>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TDateTime>> expression)
        {
            var date = Convert.ToDateTime(ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model);
            object value = date.ToShortDateString();

            return htmlHelper.TextBox(ExpressionHelper.GetExpressionText(expression), value);
        }
        #endregion


        #region NullableDateTimeFor

        /// <summary>
        /// Returns a textbox for nullable DateTime (DateTime?) model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="name">The property name which the value must be set</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        public static MvcHtmlString DateTimeNullableFor<TModel, TDateTime>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TDateTime>> expression, string name, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            object value = (metadata.Model as DateTime?).HasValue ? (metadata.Model as DateTime?).Value.ToShortDateString() : string.Empty;

            if (String.IsNullOrEmpty(name))
                name = ExpressionHelper.GetExpressionText(expression);

            return htmlHelper.TextBox(name, value, htmlAttributes);
        }

        /// <summary>
        /// Returns a textbox for nullable DateTime (DateTime?) model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="name">The property name which the value must be set</param>
        public static MvcHtmlString DateTimeNullableFor<TModel, TDateTime>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TDateTime>> expression, string name)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            object value = (metadata.Model as DateTime?).HasValue ? (metadata.Model as DateTime?).Value.ToShortDateString() : string.Empty;

            if (String.IsNullOrEmpty(name))
                name = ExpressionHelper.GetExpressionText(expression);

            return htmlHelper.TextBox(name, value);
        }

        /// <summary>
        /// Returns a textbox for nullable DateTime (DateTime?) model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        public static MvcHtmlString DateTimeNullableFor<TModel, TDateTime>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TDateTime>> expression, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            object value = (metadata.Model as DateTime?).HasValue ? (metadata.Model as DateTime?).Value.ToShortDateString() : string.Empty;

            return htmlHelper.TextBox(ExpressionHelper.GetExpressionText(expression), value, htmlAttributes);
        }

        /// <summary>
        /// Returns a textbox for nullable DateTime (DateTime?) model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        public static MvcHtmlString DateTimeNullableFor<TModel, TDateTime>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TDateTime>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            object value = (metadata.Model as DateTime?).HasValue ? (metadata.Model as DateTime?).Value.ToShortDateString() : string.Empty;

            return htmlHelper.TextBox(ExpressionHelper.GetExpressionText(expression), value);
        }
        #endregion
        */


        /// <summary>
        /// Returns a textbox for DateTime model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="name">The property name which the value must be set</param>
        /// <param name="withHours">To define if is to save the hours too</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        public static MvcHtmlString DateTimeFor<TModel, TDateTime>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TDateTime>> expression, string name, bool withHours, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            object dateValue, datetime;

            if (metadata.Model is DateTime? || metadata.Model == null)
            {
                dateValue = metadata.Model != null && (metadata.Model as DateTime?).HasValue ? (metadata.Model as DateTime?).Value.ToShortDateString() : string.Empty;
                datetime = metadata.Model != null && (metadata.Model as DateTime?).HasValue ? (metadata.Model as DateTime?).Value.ToString() : string.Empty;
            }
            else
            {
                var date = Convert.ToDateTime(ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model);
                dateValue = date.ToShortDateString();
                datetime = date.ToString();
            }
            if (String.IsNullOrEmpty(name))
                name = ExpressionHelper.GetExpressionText(expression);

            if (withHours)
            {
                var container = new TagBuilder("div");
                var datetimefor = htmlHelper.TextBox("ndef", dateValue, htmlAttributes).ToHtmlString();
                var hiddenDatetime = htmlHelper.Hidden(name, datetime, new { @class = "hiddenDateTime" }).ToHtmlString();

                //adiciona a imagem e o aviso à estrutura HTML base:
                container.InnerHtml += datetimefor;
                container.InnerHtml += hiddenDatetime;

                return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
            }
            else
            {
                var datetimefor = htmlHelper.TextBox(name, dateValue, htmlAttributes).ToHtmlString();
                return MvcHtmlString.Create(datetimefor);
            }
        }

        /// <summary>
        /// Returns a textbox for DateTime model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="name">The property name which the value must be set</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        public static MvcHtmlString DateTimeFor<TModel, TDateTime>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TDateTime>> expression, string name, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            object dateValue;

            if (metadata.Model is DateTime? || metadata.Model == null)
                dateValue = metadata.Model != null && (metadata.Model as DateTime?).HasValue ? (metadata.Model as DateTime?).Value.ToShortDateString() : string.Empty;
            else
                dateValue = Convert.ToDateTime(ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model).ToShortDateString();

            if (String.IsNullOrEmpty(name))
                name = ExpressionHelper.GetExpressionText(expression);

            var datetimefor = htmlHelper.TextBox(name, dateValue, htmlAttributes).ToHtmlString();
            return MvcHtmlString.Create(datetimefor);
        }

        /// <summary>
        /// Returns a textbox for DateTime model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="name">The property name which the value must be set</param>
        /// <param name="withHours">To define if is to save the hours too</param>
        public static MvcHtmlString DateTimeFor<TModel, TDateTime>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TDateTime>> expression, string name, bool withHours)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            object dateValue, datetime;

            if (metadata.Model is DateTime? || metadata.Model == null)
            {
                dateValue = metadata.Model != null && (metadata.Model as DateTime?).HasValue ? (metadata.Model as DateTime?).Value.ToShortDateString() : string.Empty;
                datetime = metadata.Model != null && (metadata.Model as DateTime?).HasValue ? (metadata.Model as DateTime?).Value.ToString() : string.Empty;
            }
            else
            {
                var date = Convert.ToDateTime(ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model);
                dateValue = date.ToShortDateString();
                datetime = date.ToString();
            }
            if (String.IsNullOrEmpty(name))
                name = ExpressionHelper.GetExpressionText(expression);

            if (withHours)
            {
                var container = new TagBuilder("div");
                var datetimefor = htmlHelper.TextBox("ndef", dateValue).ToHtmlString();
                var hiddenDatetime = htmlHelper.Hidden(name, datetime, new { @class = "hiddenDateTime" }).ToHtmlString();

                //adiciona a imagem e o aviso à estrutura HTML base:
                container.InnerHtml += datetimefor;
                container.InnerHtml += hiddenDatetime;

                return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
            }
            else
            {
                var datetimefor = htmlHelper.TextBox(name, dateValue).ToHtmlString();
                return MvcHtmlString.Create(datetimefor);
            }
        }

        /// <summary>
        /// Returns a textbox for DateTime model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="name">The property name which the value must be set</param>
        public static MvcHtmlString DateTimeFor<TModel, TDateTime>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TDateTime>> expression, string name)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            object dateValue;

            if (metadata.Model is DateTime? || metadata.Model == null)
                dateValue = metadata.Model != null && (metadata.Model as DateTime?).HasValue ? (metadata.Model as DateTime?).Value.ToShortDateString() : string.Empty;
            else
                dateValue = Convert.ToDateTime(ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model).ToShortDateString();

            if (String.IsNullOrEmpty(name))
                name = ExpressionHelper.GetExpressionText(expression);

            var datetimefor = htmlHelper.TextBox(name, dateValue).ToHtmlString();
            return MvcHtmlString.Create(datetimefor);
        }

        /// <summary>
        /// Returns a textbox for DateTime model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="withHours">To define if is to save the hours too</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        public static MvcHtmlString DateTimeFor<TModel, TDateTime>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TDateTime>> expression, bool withHours, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            object dateValue, datetime;

            if (metadata.Model is DateTime? || metadata.Model == null)
            {
                dateValue = metadata.Model != null && (metadata.Model as DateTime?).HasValue ? (metadata.Model as DateTime?).Value.ToShortDateString() : string.Empty;
                datetime = metadata.Model != null && (metadata.Model as DateTime?).HasValue ? (metadata.Model as DateTime?).Value.ToString() : string.Empty;
            }
            else
            {
                var date = Convert.ToDateTime(ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model);
                dateValue = date.ToShortDateString();
                datetime = date.ToString();
            }

            if (withHours)
            {
                var container = new TagBuilder("div");
                var datetimefor = htmlHelper.TextBox("ndef", dateValue, htmlAttributes).ToHtmlString();
                var hiddenDatetime = htmlHelper.Hidden(ExpressionHelper.GetExpressionText(expression), datetime, new { @class = "hiddenDateTime" }).ToHtmlString();

                //adiciona a imagem e o aviso à estrutura HTML base:
                container.InnerHtml += datetimefor;
                container.InnerHtml += hiddenDatetime;

                return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
            }
            else
            {
                var datetimefor = htmlHelper.TextBox(ExpressionHelper.GetExpressionText(expression), dateValue, htmlAttributes).ToHtmlString();
                return MvcHtmlString.Create(datetimefor);
            }
        }

        /// <summary>
        /// Returns a textbox for DateTime model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        public static MvcHtmlString DateTimeFor<TModel, TDateTime>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TDateTime>> expression, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            object dateValue;

            if (metadata.Model is DateTime? || metadata.Model == null)
                dateValue = metadata.Model != null && (metadata.Model as DateTime?).HasValue ? (metadata.Model as DateTime?).Value.ToShortDateString() : string.Empty;
            else
                dateValue = Convert.ToDateTime(ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model).ToShortDateString();

            return MvcHtmlString.Create(htmlHelper.TextBox(ExpressionHelper.GetExpressionText(expression), dateValue, htmlAttributes).ToHtmlString());
        }

        /// <summary>
        /// Returns a textbox for DateTime model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        /// <param name="withHours">To define if is to save the hours too</param>
        public static MvcHtmlString DateTimeFor<TModel, TDateTime>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TDateTime>> expression, bool withHours)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            object dateValue, datetime;

            if (metadata.Model is DateTime? || metadata.Model == null)
            {
                dateValue = metadata.Model != null && (metadata.Model as DateTime?).HasValue ? (metadata.Model as DateTime?).Value.ToShortDateString() : string.Empty;
                datetime = metadata.Model != null && (metadata.Model as DateTime?).HasValue ? (metadata.Model as DateTime?).Value.ToString() : string.Empty;
            }
            else
            {
                var date = Convert.ToDateTime(ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model);
                dateValue = date.ToShortDateString();
                datetime = date.ToString();
            }

            if (withHours)
            {
                var container = new TagBuilder("div");
                var datetimefor = htmlHelper.TextBox("ndef", dateValue).ToHtmlString();
                var hiddenDatetime = htmlHelper.Hidden(ExpressionHelper.GetExpressionText(expression), datetime, new { @class = "hiddenDateTime" }).ToHtmlString();

                //adiciona a imagem e o aviso à estrutura HTML base:
                container.InnerHtml += datetimefor;
                container.InnerHtml += hiddenDatetime;

                return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
            }
            else
            {
                var datetimefor = htmlHelper.TextBox(ExpressionHelper.GetExpressionText(expression), dateValue).ToHtmlString();
                return MvcHtmlString.Create(datetimefor);
            }
        }

        /// <summary>
        /// Returns a textbox for DateTime model properties
        /// </summary> 
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that contains the property to render</param>
        public static MvcHtmlString DateTimeFor<TModel, TDateTime>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TDateTime>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            object dateValue;

            if (metadata.Model is DateTime? || metadata.Model == null)
                dateValue = metadata.Model != null && (metadata.Model as DateTime?).HasValue ? (metadata.Model as DateTime?).Value.ToShortDateString() : string.Empty;
            else
                dateValue = Convert.ToDateTime(ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model).ToShortDateString();
    
            return MvcHtmlString.Create(htmlHelper.TextBox(ExpressionHelper.GetExpressionText(expression), dateValue).ToHtmlString());
        }

    }
}