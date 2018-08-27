using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Shared;
using Shared.Extensions;

namespace Model.ModelBinders
{
    public class CustomModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object model = base.BindModel(controllerContext, bindingContext);
            bool returnModel = false;

            if (bindingContext.ModelMetadata.IsComplexType)
            {
                if (model != null)
                {
                    bindingContext.PropertyMetadata.Values.ToList().ForEach(item =>
                    {
                        var value = bindingContext.ModelType.GetProperty(item.PropertyName).GetValue(model, null);
                        if (!Helpers.IsNullOrDefaultValue(value))
                        {
                            returnModel = true;
                        }

                    });

                    if (!returnModel)
                    {
                        bindingContext.PropertyMetadata.Values.ToList().ForEach(item =>
                        {
                            var s = string.Format("{0}.{1}", bindingContext.ModelName, item.PropertyName);
                            bindingContext.ModelState.Remove(s);

                        });
                    }
                }
            }
            else
            {
                returnModel = true;
            }

            return returnModel ? model : null;
        }

        protected override void OnModelUpdated(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            base.OnModelUpdated(controllerContext, bindingContext);
            IValidateObjectValidation(bindingContext);
        }

        private void IValidateObjectValidation(ModelBindingContext bindingContext)
        {
            var model = bindingContext.Model as IValidatableObject;
            if (model == null) return;

            var modelState = bindingContext.ModelState;

            var errors = model.Validate(new ValidationContext(model, null, null));
            foreach (var error in errors)
            {
                foreach (var memberName in error.MemberNames)
                {
                    //adiciona erros que ainda não tinham sido inseridos
                    var memberNameClone = memberName;
                    var idx = modelState.Keys.IndexOf(k => k == memberNameClone);

                    if (idx < 0)
                    {
                        modelState.Add(new KeyValuePair<string, ModelState>(memberNameClone, new ModelState()));
                        modelState.AddModelError(memberName, error.ErrorMessage);
                        continue;
                    }

                    if (modelState.Values.ToArray()[idx].Errors.Any()) continue;
                    modelState.AddModelError(memberName, error.ErrorMessage);
                }
            }
        }
    }
}
