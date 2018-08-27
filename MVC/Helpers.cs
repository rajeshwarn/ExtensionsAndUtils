using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace Shared
{
    public static class Helpers
    {
        #region Private

        /// <summary>
        /// Return a list of properties for an expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A list of properties.</returns>
        private static IEnumerable<PropertyInfo> GetProperties(Expression expression)
        {
            var memberExpression = expression as MemberExpression;
            if (memberExpression == null)
            {
                yield break;
            }

            var property = memberExpression.Member as PropertyInfo;
            foreach (var propertyInfo in GetProperties(memberExpression.Expression))
            {
                yield return propertyInfo;
            }
            yield return property;
        }

        #endregion

        #region Public

        /// <summary>
        /// Gets the model name from a lambda expression.
        /// </summary>
        /// <typeparam name="TModel">The model type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static string GetExpressionText<TModel>(Expression<Func<TModel, object>> expression)
        {
            var expr = (LambdaExpression)expression;
            if (expr.Body.NodeType == ExpressionType.Convert)
            {
                var ue = expression.Body as UnaryExpression;
                return String.Join(".", GetProperties(ue.Operand).Select(p => p.Name));
            }
            return System.Web.Mvc.ExpressionHelper.GetExpressionText(expr);
        }

        public static bool IsNullOrDefaultValue<T>(T value)
        {
            if (value == null) return true;
            if (object.Equals(value, default(T))) return true;

            //non-null nullables
            Type methodType = typeof(T);
            if (Nullable.GetUnderlyingType(methodType) != null) return false;

            //boxed value types
            Type argumentType = value.GetType();
            if (argumentType.IsValueType && argumentType != methodType)
            {
                object obj = Activator.CreateInstance(value.GetType());
                return obj.Equals(value);
            }

            return false;
        }

        public static string RenderRazorViewToString(ControllerContext context, string viewName, object model)
        {
            context.Controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                var viewContext = new ViewContext(context, viewResult.View, context.Controller.ViewData, context.Controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(context, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
		
		
        public static MvcHtmlString ToCSV(this HtmlHelper helper, IEnumerable<string> list)
        {
            return new MvcHtmlString(string.Join(", ", list));
        }

        #endregion
    }
}
