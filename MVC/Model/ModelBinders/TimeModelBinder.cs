using System.Linq;
using System.Web.Mvc;
using Shared.Extensions;

namespace Model.ModelBinders
{
    public class TimeModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext);
            bool returnModel = false;
            int nullPropertiesCount = 0;

            var time = model as Time;
            if (time != null)
            {
                if (time.Value == null)
                {
                    nullPropertiesCount++;
                }
                else
                {
                    returnModel = true;
                }

                if (time.TimeUnit == null)
                {
                    nullPropertiesCount++;
                }
                else
                {
                    returnModel = true;
                }
            }

            if (nullPropertiesCount == 1)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, ModelBindersResources.TimeModelBinderRequiredAllFields);
            }
            else if (nullPropertiesCount == 2 && bindingContext.ModelMetadata.IsRequired)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName,
                    string.Format(ModelBindersResources.TimeModelBinderRequiredField, bindingContext.ModelMetadata.DisplayName));
            }

            if (bindingContext.ModelState.CheckIfHasErrorsFor(bindingContext.ModelName))
            {
                bindingContext.PropertyMetadata.Values.ToList().ForEach(item =>
                {
                    var s = string.Format("{0}.{1}", bindingContext.ModelName, item.PropertyName);
                    bindingContext.ModelState.AddModelError(s, " ");
                });
            }

            if (bindingContext.ModelMetadata.IsRequired)
            {
                return model;
            }
            
            return returnModel ? model : null;
        }
    }
}