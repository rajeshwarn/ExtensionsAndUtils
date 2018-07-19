using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Shared.Validation
{
    public static class ControllerValidation
    {
        public static void CleanNonValidatePropertiesErrors(ControllerContext context)
        {
            var modelState = context.Controller.ViewData.ModelState;
            var valueProvider = context.Controller.ValueProvider;

            var noValidatePropertyKeys = modelState.Keys.Where(x => !valueProvider.ContainsPrefix(x));
            foreach (var key in noValidatePropertyKeys)
                modelState[key].Errors.Clear();
        }

        public static void CleanNullPropertiesFromList(IList list)
        {
            if (list != null)
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] == null)
                    {
                        list.RemoveAt(i);
                        i--;
                    }
                }
        }
        
        public static void CleanNullProperties(object obj)
        {
            if (obj != null)
            {
                    foreach (var aux in from ob in obj.GetType().GetProperties() where ob.PropertyType.Name == typeof (List<>).Name select ob.GetValue(obj, null))
                    {
                        CleanNullPropertiesFromList((IList)aux);
                    }
            }
        }

    }
    public class CleanNonValidatePropertiesErrors : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var modelState = filterContext.Controller.ViewData.ModelState;
            var valueProvider = filterContext.Controller.ValueProvider;

            var keysWithNoIncomingValue = modelState.Keys.Where(x => !valueProvider.ContainsPrefix(x));
            foreach (var key in keysWithNoIncomingValue)
                modelState[key].Errors.Clear();
        }
    }

    public class ValidateAjaxAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
                return;

            var modelState = filterContext.Controller.ViewData.ModelState;
            var valueProvider = filterContext.Controller.ValueProvider;

            var keysWithNoIncomingValue = modelState.Keys.Where(x => !valueProvider.ContainsPrefix(x));
            foreach (var key in keysWithNoIncomingValue)
                modelState[key].Errors.Clear();


            if (!modelState.IsValid)
            {
                var errorModel =
                        from x in modelState.Keys
                        where modelState[x].Errors.Count > 0
                        select new
                        {
                            key = x,
                            errors = modelState[x].Errors.
                                                   Select(y => y.ErrorMessage).
                                                   ToArray()
                        };
                filterContext.Result = new JsonResult() { Data = errorModel };
            }
        }
    }
}
