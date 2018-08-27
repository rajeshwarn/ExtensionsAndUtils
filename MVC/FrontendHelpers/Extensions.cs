using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace Helpers
{
    public static class Extensions
    {
        #region ModelState

        public static void ClearErrorsFor<TModel>(this ModelStateDictionary modelState, Expression<Func<TModel, object>> expression)
        {
            if (expression != null)
            {
                string expressionText = ExpressionHelper.GetExpressionText(expression);

                if (!string.IsNullOrEmpty(expressionText))
                    modelState.Where(item => item.Key.StartsWith(expressionText)).ForEach(item => item.Value.Errors.Clear());
            }
        }

        #endregion
    }
}