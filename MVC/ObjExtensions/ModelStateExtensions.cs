using System;
using System.Linq;
using System.Linq.Expressions;

namespace Shared.Extensions
{
    public static class ModelStateExtensions
    {
        public static void ClearErrorsFor<TModel>(this System.Web.Mvc.ModelStateDictionary modelState, Expression<Func<TModel, object>> modelExpression)
        {
            string expressionText = Helpers.GetExpressionText(modelExpression);
            if (!string.IsNullOrEmpty(expressionText))
            {
                modelState.Where(item => item.Key.Contains(expressionText))
                    .ToList()
                    .ForEach(item => item.Value.Errors.Clear());
            }
        }

        public static void ClearErrorsFor<TModel, TException>(this System.Web.Mvc.ModelStateDictionary modelState, Expression<Func<TModel, object>> modelExpression)
        {
            string expressionText = Helpers.GetExpressionText(modelExpression);
            if (!string.IsNullOrEmpty(expressionText))
            {
                modelState.Where(item => item.Key.Contains(expressionText))
                          .ToList()
                          .ForEach(item => item.Value.Errors.Where(e => e.Exception != null && e.Exception.GetType() == typeof(TException))
                                                            .ToList()
                                                            .ForEach(e => item.Value.Errors.Remove(e)));
            }
        }

        public static void ClearNonExceptionErrorsFor<TModel>(this System.Web.Mvc.ModelStateDictionary modelState, Expression<Func<TModel, object>> modelExpression)
        {
            string expressionText = Helpers.GetExpressionText(modelExpression);
            if (!string.IsNullOrEmpty(expressionText))
            {
                modelState.Where(item => item.Key.Contains(expressionText))
                          .ToList()
                          .ForEach(item => item.Value.Errors.Where(e => e.Exception == null)
                                                            .ToList()
                                                            .ForEach(e => item.Value.Errors.Remove(e)));
            }
        }

        public static bool CheckIfHasErrorsFor<TModel>(this System.Web.Mvc.ModelStateDictionary modelState,
            Expression<Func<TModel, object>> expression)
        {
            string expressionText = Helpers.GetExpressionText(expression);
            return !string.IsNullOrEmpty(expressionText) &&
                   modelState.Where(item => item.Key.StartsWith(expressionText))
                       .Any(item => item.Value.Errors.Count != 0);
        }

        public static bool CheckIfHasErrorsFor(this System.Web.Mvc.ModelStateDictionary modelState, string expressionText)
        {
            return (!modelState.IsValid || !string.IsNullOrEmpty(expressionText)) &&
                   modelState.Where(item => item.Key.StartsWith(expressionText))
                       .Any(item => item.Value.Errors.Count != 0);
        }
    }
}