using System;
using System.Globalization;
using System.Web.Mvc;
using Shared.Extensions;

namespace Model.ModelBinders
{
    public class NumericModelBinder<T> : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var modelState = new ModelState { Value = valueResult };
            object actualValue = null;

            try
            {
                var result = valueResult.AttemptedValue;
                if (!string.IsNullOrEmpty(result))
                {
                    if (result.IsNumeric())
                    {
                        actualValue = (T)Convert.ChangeType(result.Replace(',', '.'), typeof(T).UnderlyingType(), CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
            }
            catch (FormatException e)
            {
                modelState.Errors.Add(new ModelError(e, ModelBindersResources.NumericModelBinderMustBeNumeric));
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }
}
