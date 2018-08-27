﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Attributes
{
    public class RequiredIfAttribute : RequiredAttribute
    {
        private String PropertyName { get; set; }
        private Object DesiredValue { get; set; }

        public RequiredIfAttribute(String propertyName, Object desiredValue)
        {
            PropertyName = propertyName;
            DesiredValue = desiredValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            Object instance = context.ObjectInstance;
            Type type = instance.GetType();
            Object propertyValue = type.GetProperty(PropertyName).GetValue(instance, null);
            if (propertyValue != null)
            {
                if (propertyValue.ToString() == DesiredValue.ToString())
                {
                    ValidationResult result = base.IsValid(value, context);
                    return result;
                }
            }
            return ValidationResult.Success;
        }
    }

    
}
