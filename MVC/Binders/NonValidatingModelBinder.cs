using System.ComponentModel;
using System.Web.Mvc;

namespace Frontend.Binder
{
    public class NonValidatingModelBinder : DefaultModelBinder
    {
        protected override bool OnPropertyValidating(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, object value)
        {
            if (controllerContext.HttpContext.Request.IsAjaxRequest())
                return false;
            
            return true;
        }
    }
}